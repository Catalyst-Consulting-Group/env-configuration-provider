using System;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace CatConsult.EnvConfigurationProvider
{
    public static class EnvConfigurationProviderExtensions
    {
        /// <summary>
        /// Adds an <see cref="EnvConfigurationProvider"/> that reads configuration values from a .env file and the operating system
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to</param>
        /// <param name="config">An action that provides an <see cref="IEnvConfigurationBuilder"/> for chaining provider configuration</param>
        /// <returns>The <see cref="IConfigurationBuilder"/></returns>
        public static IConfigurationBuilder AddEnvs(this IConfigurationBuilder configurationBuilder, Action<IEnvConfigurationBuilder> config)
        {
            var provider = new EnvConfigurationProvider();
            var source = new EnvConfigurationSource(provider);

            var builder = new EnvConfigurationBuilder(provider);
            config(builder);

            return configurationBuilder.Add(source);
        }
    }
}
