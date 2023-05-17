namespace PlantManagerServer.Models;

public record JwtSetting
{
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string Secret { get; set; } = "";
}