using Microsoft.EntityFrameworkCore;
using PlantManagerServer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //��ֹ������ĸ��ΪСд
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Add DbContext configuration
builder.Services.AddDbContext<PlantDbContext>(opt => opt.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
