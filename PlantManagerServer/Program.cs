using Microsoft.EntityFrameworkCore;
using Minio;
using PlantManagerServer.Models;

var builder = WebApplication.CreateBuilder(args);

try
{
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

    //app.Run();
    await app.RunAsync();
}
catch (Exception e)
{
    Console.WriteLine(e);
    //throw;
}