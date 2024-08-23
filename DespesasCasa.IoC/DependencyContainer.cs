using AutoMapper;
using DespesasCasa.Data.Context;
using DespesasCasa.Data.Repository;
using DespesasCasa.Domain.Interface.Repository;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper.Extensions.ExpressionMapping;
using DespesasCasa.Domain.Mapping;

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

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        // Automapper
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddExpressionMapping();
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }

    //Iniciar o banco de dados com as migrations ao iniciar a aplicação
    public static void InitializeDatabase(IServiceProvider serviceProvider)
    {
        using (var serviceScope = serviceProvider.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<PostgresDbContext>();
            context?.Database.Migrate();
        }
    }
}
