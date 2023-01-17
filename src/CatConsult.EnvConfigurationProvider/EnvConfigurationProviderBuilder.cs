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
            _provider.Mappings.Add(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                IsRequired = true,
            });

            return this;
        }

        public IEnvConfigurationProviderBuilder AddOptionalEnv(string env, string configurationKey, string defaultValue = null)
        {
            _provider.Mappings.Add(new EnvMapping
            {
                Env = env,
                ConfigurationKey = configurationKey,
                DefaultValue = defaultValue,
            });

            return this;
        }
    }
}
