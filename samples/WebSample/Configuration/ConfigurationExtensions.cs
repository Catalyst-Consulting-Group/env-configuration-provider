using CatConsult.EnvConfigurationProvider;
using CatConsult.EnvConfigurationProvider.Models;

namespace WebSample.Configuration;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddEnvs(this IConfigurationBuilder builder) =>
        builder.AddEnvs(config => config
            .AddRequiredEnv("GREETING_FORMAT", "Greeting:Format")
            .AddOptionalEnv("GREETING_NAME", "Greeting:Name", "EnvConfigurationProvider")
            .AddCustomMapper(envs => new ConfigurationEntry(
                "ConnectionStrings:DefaultConnection",
                $"Server={envs["DB_HOST"]};Port={envs["DB_PORT"]};Database={envs["DB_DATABASE"]};User Id={envs["DB_USER"]};Password={envs["DB_PASSWORD"]};"
            ))
        );
}
