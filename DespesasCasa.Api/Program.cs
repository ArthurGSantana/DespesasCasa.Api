using System.Text;
using System.Text.Json.Serialization;
using DespesasCasa.Api.Environment;
using DespesasCasa.Api.Filters;
using DespesasCasa.Domain.Config;
using DespesasCasa.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

const string allowedOriginsName = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

DependencyContainer.RegisterServices(builder.Services, builder.Configuration.GetConnectionString("PostgresConnectionString"));

//Add Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
    options.Filters.Add(typeof(ValidationFilter));
}).AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
            };
        }
    );

builder.Services.Configure<ApplicationConfig>(configuration);

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

await app.RunAsync();
