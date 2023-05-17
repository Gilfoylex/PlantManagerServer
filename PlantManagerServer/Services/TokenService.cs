using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PlantManagerServer.Models;

namespace PlantManagerServer.Services;

public class TokenService
{
    private readonly JwtSetting _jwtSetting;
    public TokenService(JwtSetting jwtSetting)
    {
        _jwtSetting = jwtSetting;
    }
    
    private const int ExpirationMinutes = 30;

    public string CreateToken(UserTable user)
    {
        var expirationTime = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreatJwtSecurityToken(CreateClaims(user), CreateSigningCredentials(), expirationTime);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    
    private JwtSecurityToken CreatJwtSecurityToken(IEnumerable<Claim> claims, SigningCredentials credentials, DateTime expirationTime)
    {
        return new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: expirationTime,
            signingCredentials: credentials
        );
    }
    
    private SigningCredentials CreateSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSetting.Secret);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    
    private IEnumerable<Claim> CreateClaims(UserTable user)
    {
        return new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.NameIdentifier, user.UserId.ToString())
        };
    }
}