using CatConsult.EnvConfigurationProvider.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace CatConsult.EnvConfigurationProvider.Tests;

public class EnvConfigurationProviderTests
{
    [Fact]
    public void Should_Map_Envs_From_File()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvs(config => config
                .AddRequiredEnv("FOO_API_URL", "Foo:ApiUrl")
                .AddRequiredEnv("FOO_API_KEY", "Foo:ApiKey")
                .AddOptionalEnv("BAZ", "Foo:OptionalWithDefault", "DifferentDefaultValue")
                .AddOptionalEnv("BAZ", "Foo:OptionalWithNull")
            )
            .Build();

        var options = configuration.GetSection("Foo").Get<FooOptions>();

        options.Should().BeEquivalentTo(new FooOptions
        {
            ApiUrl = "https://api.foo.com",
            ApiKey = "1234",
            OptionalWithDefault = "DifferentDefaultValue",
            OptionalWithNull = null,
        });
    }

    [Fact]
    public void Should_Throw_On_Missing_Required_Envs()
    {
        var act = () =>
            new ConfigurationBuilder()
                .AddEnvs(config => config.AddRequiredEnv("MISSING", "Foo:ApiUrl"))
                .Build();

        act.Should().Throw<MappingException>()
            .WithMessage("Environment Key: 'MISSING' was not found");
    }

    [Fact]
    public void Should_Map_Custom_Mapper()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvs(config => config
                .AddCustomMapper(envs =>
                    new ConfigurationEntry("Greeting", $"Hello, {envs["FIRST_NAME"]} {envs["LAST_NAME"]}")
                )
            )
            .Build();

        configuration["Greeting"].Should().Be("Hello, John Smith");
    }

    [Fact]
    public void Should_Map_Custom_Multi_Mapper()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvs(config => config
                .AddCustomMultiMapper(envs =>
                    new[]
                    {
                        new ConfigurationEntry("Greetings:English", $"Hello, {envs["FIRST_NAME"]} {envs["LAST_NAME"]}"),
                        new ConfigurationEntry("Greetings:French", $"Bonjour, {envs["FIRST_NAME"]} {envs["LAST_NAME"]}"),
                    }
                )
            )
            .Build();

        var greetings = configuration.GetSection("Greetings").Get<GreetingOptions>();

        greetings.Should().BeEquivalentTo(new GreetingOptions
        {
            English = "Hello, John Smith",
            French = "Bonjour, John Smith"
        });
    }
}

public class FooOptions
{
    public required string ApiUrl { get; set; }

    public required string ApiKey { get; set; }

    public string OptionalWithDefault { get; set; } = "DefaultValue";

    public string? OptionalWithNull { get; set; }
}

public class GreetingOptions
{
    public required string English { get; set; }

    public required string French { get; set; }
}
