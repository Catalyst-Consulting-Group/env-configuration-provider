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
    public void Should_Map_Envs_From_Operating_System()
    {
        Environment.SetEnvironmentVariable("FOOBAR_TEST_ENV", "Testing");
        
        var configuration = new ConfigurationBuilder()
            .AddEnvs(config => config
                .AddRequiredEnv("FOOBAR_TEST_ENV", "Foobar")
            )
            .Build();

        configuration["Foobar"].Should().Be("Testing");
    }
    
    [Fact]
    public void Should_Not_Override_Envs_From_Operating_System()
    {
        Environment.SetEnvironmentVariable("OVERRIDE_ME", "bar");
        
        var configuration = new ConfigurationBuilder()
            .AddEnvs(config => config
                .AddRequiredEnv("OVERRIDE_ME", "OverrideMe")
            )
            .Build();

        configuration["OverrideMe"].Should().Be("bar");
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
    public void Should_Map_Conditional_Envs()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvs(config => config
                .AddRequiredEnv("FIRST_NAME", "FirstName1", env => env.StartsWith("J"))
                .AddOptionalEnv("MIDDLE_NAME", "MiddleName1", env => !string.IsNullOrEmpty(env))
                .AddRequiredEnv("LAST_NAME", "LastName1", env => env.StartsWith("S"))
                .AddRequiredEnv("FIRST_NAME", "FirstName2", env => env.StartsWith("F"))
                .AddOptionalEnv("MIDDLE_NAME", "MiddleName2", _ => true, "Foobar")
                .AddRequiredEnv("LAST_NAME", "LastName2", env => env.StartsWith("B"))
            )
            .Build();

        configuration["FirstName1"].Should().Be("John");
        configuration["MiddleName1"].Should().BeNull();
        configuration["LastName1"].Should().Be("Smith");

        configuration["FirstName2"].Should().BeNull();
        configuration["MiddleName2"].Should().Be("Foobar");
        configuration["FirstName2"].Should().BeNull();
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
