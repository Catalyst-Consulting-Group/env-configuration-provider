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
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to</param>
        /// <param name="configureProvider">An action that provides an <see cref="IEnvConfigurationProviderBuilder"/> for chaining provider configuration</param>
        /// <returns>The <see cref="IConfigurationBuilder"/></returns>
        public static IConfigurationBuilder AddEnvs(this IConfigurationBuilder builder, Action<IEnvConfigurationProviderBuilder> configureProvider)
        {
            var provider = new EnvConfigurationProvider();

            builder.Add(
                new EnvConfigurationSource(provider)
            );

            configureProvider(new EnvConfigurationProviderBuilder(provider));

            return builder;
        }
    }
}
