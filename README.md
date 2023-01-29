# CatConsult.EnvConfigurationProvider

This repository contains a [.NET Configuration Provider](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration-providers)
that offers a fluent API for loading and transforming environment variables from the OS and `.env` files.

## Overview

In many other languages and frameworks, `.env`s are text files that contain key-value pairs of configuration data or secrets.
These files are not typically checked in to source control and can vary between developers.

Some tools, such as Docker Compose, can read `.env` as well, making the file an enticing location for storing shared configuration
that both Docker and your .NET app can use.

This provider offers the convenience of loading configuration and secrets from `.env` files and also from the local operating system.
System variables have a higher precedence than ones found in `.env` files, making it easy to override them where needed.

In most production workloads, configuration and secrets should be sourced from more secure and flexible sources,
such as Azure Key Vault or AWS Secrets Manager. As such, this provider is intended to be used primarily in development and testing scenarios.

## Usage

First, download the provider from NuGet:

```shell
dotnet add package CatConsult.EnvConfigurationProvider
```

Then add the provider using the `AddEnvs()` extension method:

```csharp
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
```

In this example taken from the [Web Sample](samples/WebSample/Program.cs), we are adding a required env, an optional env with a default value,
and a custom mapping function that pulls multiple envs at once to build a connection string.

`AddRequiredEnv` and `AddOptionalEnv` both come with overloads that allow a conditional function to determine if the variable should be loaded into configuration.
This can be useful if you want to prevent clobbering other providers' values if they are already set.

You can chain as many functions as you want. If you are concerned about this cluttering up your `Program.cs`, you can create a custom
extension method in your project that performs the configuration. See the [Web Sample](samples/WebSample/Configuration/ConfigurationExtensions.cs)
for an example.

You can also create custom extension methods for `IEnvConfigurationBuilder` that can encapsulate custom logic. For example, 
if multiple projects need access to the same connection string, you could write an extension method that wraps the `AddCustomMapper` function above
and share it in a common class library.
