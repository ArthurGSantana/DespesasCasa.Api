using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DespesasCasa.Data.Context;

public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }

        // Reflection para chamar os métodos Seed das classes que implementam ISeed
        // var seeders = Assembly.GetExecutingAssembly().GetTypes()
        //                     .Where(t => typeof(ISeed).IsAssignableFrom(t) && !t.IsInterface)
        //                     .Select(Activator.CreateInstance)
        //                     .Cast<ISeed>();

        // foreach (var seeder in seeders)
        // {
        //     seeder.Seed(modelBuilder);
        // }

        base.OnModelCreating(modelBuilder);
    }
}
