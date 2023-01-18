using CatConsult.EnvConfigurationProvider.Models;

namespace CatConsult.EnvConfigurationProvider
{
    public class EnvConfigurationProviderBuilder : IEnvConfigurationProviderBuilder
    {
        private readonly EnvConfigurationProvider _provider;

        public EnvConfigurationProviderBuilder(EnvConfigurationProvider provider)
        {
            _provider = provider;
        }

        public IEnvConfigurationProviderBuilder AddRequiredEnv(string env, string configurationKey)
        {
            _provider.AddMapping(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                IsRequired = true,
            });

            return this;
        }

        public IEnvConfigurationProviderBuilder AddOptionalEnv(string env, string configurationKey, string defaultValue = null)
        {
            _provider.AddMapping(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                DefaultValue = defaultValue,
            });

            return this;
        }

        public IEnvConfigurationProviderBuilder AddCustomMapper(CustomEnvMapper mapper)
        {
            _provider.AddCustomMapper(mapper);

            return this;
        }

        public IEnvConfigurationProviderBuilder AddCustomMultiMapper(CustomEnvMultiMapper mapper)
        {
            _provider.AddCustomMultiMapper(mapper);

            return this;
        }
    }
}
