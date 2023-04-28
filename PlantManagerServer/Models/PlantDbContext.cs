using Microsoft.EntityFrameworkCore;
using Shared.Models;
namespace PlantManagerServer.Models;

public class PlantDbContext : DbContext
{

    public PlantDbContext(DbContextOptions<PlantDbContext> options) : base(options)
    {

    }

    public DbSet<PlantTable> PlantEntities { get; set; }
}
