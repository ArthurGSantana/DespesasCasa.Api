using DespesasCasa.Api.Environment;
using DespesasCasa.IoC;

const string allowedOriginsName = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

DependencyContainer.RegisterServices(builder.Services, builder.Configuration.GetConnectionString("PostgresConnectionString"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Setup
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOriginsName,
        cors =>
        {
            List<string> allowedOrigins = new List<string>();

            // Debug, Development and Quality Environment Allow for HTTP
            if (builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == EnvironmentNames.Debug || builder.Environment.EnvironmentName == EnvironmentNames.Quality)
            {
                allowedOrigins.Add("http://localhost:4200");
            }

            if (allowedOrigins.Any())
            {
                cors.WithOrigins(allowedOrigins.ToArray())
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DependencyContainer.InitializeDatabase(app.Services);

app.UseCors(allowedOriginsName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
