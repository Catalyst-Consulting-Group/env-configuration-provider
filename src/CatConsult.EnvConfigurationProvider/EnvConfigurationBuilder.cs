using System;
using CatConsult.EnvConfigurationProvider.Models;

namespace CatConsult.EnvConfigurationProvider
{
    public class EnvConfigurationBuilder : IEnvConfigurationBuilder
    {
        private readonly EnvConfigurationProvider _provider;

        public EnvConfigurationBuilder(EnvConfigurationProvider provider)
        {
            _provider = provider;
        }

        public IEnvConfigurationBuilder AddEnv(EnvMapping mapping)
        {
            _provider.AddMapping(mapping);

            return this;
        }

        public IEnvConfigurationBuilder AddRequiredEnv(string env, string configurationKey)
        {
            return AddEnv(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                IsRequired = true,
            });
        }

        public IEnvConfigurationBuilder AddRequiredEnv(string env, string configurationKey, Func<string, bool> condition)
        {
            return AddEnv(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                IsRequired = true,
                Condition = condition,
            });
        }

        public IEnvConfigurationBuilder AddOptionalEnv(string env, string configurationKey, string defaultValue = null)
        {
            return AddEnv(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                IsRequired = false,
                DefaultValue = defaultValue,
            });
        }

        public IEnvConfigurationBuilder AddOptionalEnv(string env, string configurationKey, Func<string, bool> condition, string defaultValue = null)
        {
            return AddEnv(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                IsRequired = false,
                DefaultValue = defaultValue,
                Condition = condition,
            });
        }

        public IEnvConfigurationBuilder AddCustomMapper(CustomEnvMapper mapper)
        {
            _provider.AddCustomMapper(mapper);

            return this;
        }

        public IEnvConfigurationBuilder AddCustomMultiMapper(CustomEnvMultiMapper mapper)
        {
            _provider.AddCustomMultiMapper(mapper);

            return this;
        }
    }
}
