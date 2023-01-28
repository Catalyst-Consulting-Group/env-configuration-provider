using System;
using CatConsult.EnvConfigurationProvider.Models;

namespace CatConsult.EnvConfigurationProvider
{
    /// <summary>
    /// Provides a fluent API to configure the <see cref="EnvConfigurationProvider"/>
    /// </summary>
    public interface IEnvConfigurationBuilder
    {
        /// <summary>
        /// Adds an environment variable mapping
        /// </summary>
        /// <param name="mapping">The environment variable mapping</param>
        /// <returns>An <see cref="IEnvConfigurationBuilder"/> for chaining further calls</returns>
        IEnvConfigurationBuilder AddEnv(EnvMapping mapping);
        
        /// <summary>
        /// Adds a required environment variable mapping directive to the provider.
        /// If the environment variable is not found, the provider will throw an error when the configuration is loaded.
        /// </summary>
        /// <param name="env">The key or name of the environment variable</param>
        /// <param name="configurationKey">The <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> key to bind the value to</param>
        /// <returns>An <see cref="IEnvConfigurationBuilder"/> for chaining further calls</returns>
        IEnvConfigurationBuilder AddRequiredEnv(string env, string configurationKey);

        /// <summary>
        /// Adds a required environment variable mapping directive to the provider if <paramref name="condition"/> returns true.
        /// If the environment variable is not found, the provider will throw an error when the configuration is loaded.
        /// </summary>
        /// <param name="env">The key or name of the environment variable</param>
        /// <param name="configurationKey">The <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> key to bind the value to</param>
        /// <param name="condition">A function that returns true if the environment variable should be added to the configuration</param>
        /// <returns>An <see cref="IEnvConfigurationBuilder"/> for chaining further calls</returns>
        IEnvConfigurationBuilder AddRequiredEnv(string env, string configurationKey, Func<string, bool> condition);

        /// <summary>
        /// Adds an optional environment variable mapping directive to the provider
        /// </summary>
        /// <param name="env">The key or name of the environment variable</param>
        /// <param name="configurationKey">The <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> key to bind the value to</param>
        /// <param name="defaultValue">A default value that will be set if the environment variable is not found. Defaults to null</param>
        /// <returns>An <see cref="IEnvConfigurationBuilder"/> for chaining further calls</returns>
        IEnvConfigurationBuilder AddOptionalEnv(string env, string configurationKey, string defaultValue = null);

        /// <summary>
        /// Adds an optional environment variable mapping directive to the provider if <paramref name="condition"/> returns true
        /// </summary>
        /// <param name="env">The key or name of the environment variable</param>
        /// <param name="configurationKey">The <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> key to bind the value to</param>
        /// <param name="condition">A function that returns true if the environment variable should be added to the configuration</param>
        /// <param name="defaultValue">A default value that will be set if the environment variable is not found. Defaults to null</param>
        /// <returns>An <see cref="IEnvConfigurationBuilder"/> for chaining further calls</returns>
        IEnvConfigurationBuilder AddOptionalEnv(string env, string configurationKey, Func<string, bool> condition, string defaultValue = null);

        /// <summary>
        /// Adds a custom environment variable mapper to the provider
        /// </summary>
        /// <param name="mapper">A <see cref="CustomEnvMapper"/> delegate</param>
        /// <returns>An <see cref="IEnvConfigurationBuilder"/> for chaining further calls</returns>
        IEnvConfigurationBuilder AddCustomMapper(CustomEnvMapper mapper);

        /// <summary>
        /// Adds a custom environment variable multi-mapper to the provider
        /// </summary>
        /// <param name="mapper">A <see cref="CustomEnvMultiMapper"/> delegate</param>
        /// <returns>An <see cref="IEnvConfigurationBuilder"/> for chaining further calls</returns>
        IEnvConfigurationBuilder AddCustomMultiMapper(CustomEnvMultiMapper mapper);
    }
}
