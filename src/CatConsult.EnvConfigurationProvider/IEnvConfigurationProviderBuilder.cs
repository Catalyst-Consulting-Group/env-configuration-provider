namespace CatConsult.EnvConfigurationProvider
{
    /// <summary>
    /// Provides a fluent API to configure the <see cref="EnvConfigurationProvider"/>
    /// </summary>
    public interface IEnvConfigurationProviderBuilder
    {
        /// <summary>
        /// Adds a required environment variable mapping directive to the provider.
        /// If the environment variable is not found, the provider will throw an error when the configuration is loaded.
        /// </summary>
        /// <param name="env">The key or name of the environment variable</param>
        /// <param name="configurationKey">The <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> key to bind the value to</param>
        /// <returns>An <see cref="IEnvConfigurationProviderBuilder"/> for chaining further calls</returns>
        IEnvConfigurationProviderBuilder AddRequiredEnv(string env, string configurationKey);

        /// <summary>
        /// Adds an optional environment variable mapping directive to the provider
        /// </summary>
        /// <param name="env">The key or name of the environment variable</param>
        /// <param name="configurationKey">The <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> key to bind the value to</param>
        /// <param name="defaultValue">A default value that will be set if the environment variable is not found. Defaults to null</param>
        /// <returns>An <see cref="IEnvConfigurationProviderBuilder"/> for chaining further calls</returns>
        IEnvConfigurationProviderBuilder AddOptionalEnv(string env, string configurationKey, string defaultValue = null);

        /// <summary>
        /// Adds a custom environment variable mapper to the provider
        /// </summary>
        /// <param name="mapper">A <see cref="CustomEnvMapper"/> delegate</param>
        /// <returns>An <see cref="IEnvConfigurationProviderBuilder"/> for chaining further calls</returns>
        IEnvConfigurationProviderBuilder AddCustomMapper(CustomEnvMapper mapper);

        /// <summary>
        /// Adds a custom environment variable multi-mapper to the provider
        /// </summary>
        /// <param name="mapper">A <see cref="CustomEnvMultiMapper"/> delegate</param>
        /// <returns>An <see cref="IEnvConfigurationProviderBuilder"/> for chaining further calls</returns>
        IEnvConfigurationProviderBuilder AddCustomMultiMapper(CustomEnvMultiMapper mapper);
    }
}
