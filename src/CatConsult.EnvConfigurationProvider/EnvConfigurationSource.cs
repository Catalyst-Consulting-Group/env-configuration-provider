using Microsoft.Extensions.Configuration;

namespace CatConsult.EnvConfigurationProvider
{
    public class EnvConfigurationSource : IConfigurationSource
    {
        private readonly EnvConfigurationProvider _provider;

        public EnvConfigurationSource(EnvConfigurationProvider provider)
        {
            _provider = provider;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => _provider;
    }
}
