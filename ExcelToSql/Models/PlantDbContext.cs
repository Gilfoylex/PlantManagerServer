using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ExcelToSql.Models;

public class PlantDbContext : DbContext
{
    private readonly string _host;
    private readonly int _port = 5432;
    private readonly string _dataBaseName;
    private readonly string _userName;
    private readonly string _password;

    public DbSet<PlantTable> PlantEntities { get; set; }

    public PlantDbContext(string databaseName, string user, string password, string host = "localhost", int port = 5432)
    {
        _host = host;
        _port = port;
        _dataBaseName = databaseName;
        _userName = user;
        _password = password;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host={_host};Port={_port};Database={_dataBaseName};Username={_userName};Password={_password}");
    }
}