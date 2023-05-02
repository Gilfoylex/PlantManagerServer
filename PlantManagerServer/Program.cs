using Microsoft.EntityFrameworkCore;
using PlantManagerServer.Models;

var builder = WebApplication.CreateBuilder(args);

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