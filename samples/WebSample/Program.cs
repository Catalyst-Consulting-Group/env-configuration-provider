using CatConsult.EnvConfigurationProvider;
using CatConsult.EnvConfigurationProvider.Models;
using Microsoft.Extensions.Options;
using WebSample.Options;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration
        .AddEnvs(config => config
            .AddRequiredEnv("GREETING_FORMAT", "Greeting:Format")
            .AddOptionalEnv("GREETING_NAME", "Greeting:Name", "EnvConfigurationProvider")
            .AddCustomMapper(envs => new ConfigurationEntry(
                "ConnectionStrings:DefaultConnection",
                $"Server={envs["DB_HOST"]};Port={envs["DB_PORT"]};Database={envs["DB_DATABASE"]};User Id={envs["DB_USER"]};Password={envs["DB_PASSWORD"]};"
            ))
        );
}

builder.Services
    .AddOptions<GreetingOptions>()
    .BindConfiguration("Greeting")
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

app.MapGet("/", (IConfiguration config, IOptions<GreetingOptions> options) =>
    new
    {
        Greeting = string.Format(options.Value.Format, options.Value.Name),
        Database = config.GetConnectionString("DefaultConnection"),
    }
);

app.Run();
