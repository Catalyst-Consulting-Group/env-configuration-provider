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

        public IEnvConfigurationBuilder AddRequiredEnv(string env, string configurationKey)
        {
            _provider.AddMapping(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                IsRequired = true,
            });

            return this;
        }

        public IEnvConfigurationBuilder AddOptionalEnv(string env, string configurationKey, string defaultValue = null)
        {
            _provider.AddMapping(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                DefaultValue = defaultValue,
            });

            return this;
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
