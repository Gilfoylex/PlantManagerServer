using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Minio;
using PlantManagerServer.Models;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{

    Log.Information("Start Plant Manager Server");

    var builder = WebApplication.CreateBuilder(args);

    // use serilog
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        /*.WriteTo.Console()*/); //这里如果也输出到console的话会输出两遍

    // Add services to the container.
    // builder.Services.AddControllers()
    //     .AddJsonOptions(options =>
    //     {
    //         //防止将json首字母小写
    //         options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //     });

    // Add DbContext configuration
    builder.Services.AddDbContext<PlantDbContext>(opt => opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add Minio configuration
    var minioConfig = builder.Configuration.GetSection("Minio").Get<MinioConfig>();
    // Register MinioClient as singleton
    builder.Services.AddSingleton(x =>
        new MinioClient()
            .WithEndpoint(minioConfig.Endpoint)
            .WithCredentials(minioConfig.AccessKey, minioConfig.SecretKey)
            .WithSSL(minioConfig.Secure)
            .Build());
    
    // Add jwt
    // 读取配置文件
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                //ValidIssuer = 
            };
        });

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Configure the HTTP request pipeline.

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    Log.Information("Stopped cleanly");
}
catch (Exception e)
{
    Log.Fatal(e, "An unhandled exception occured during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}