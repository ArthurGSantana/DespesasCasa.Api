using DespesasCasa.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DespesasCasa.IoC;

public static class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services, string? postGresConnectionString)
    {
        //Context
        services.AddDbContext<PostgresDbContext>(options =>
        {
            if (postGresConnectionString != null)
            {
                options.UseNpgsql(postGresConnectionString);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });
    }

    //Iniciar o banco de dados com as migrations ao iniciar a aplicação
    // public static void InitializeDatabase(IServiceProvider serviceProvider)
    // {
    //     using (var serviceScope = serviceProvider.CreateScope())
    //     {
    //         var context = serviceScope.ServiceProvider.GetService<PostgresDbContext>();
    //         context?.Database.Migrate();
    //     }
    // }
}
