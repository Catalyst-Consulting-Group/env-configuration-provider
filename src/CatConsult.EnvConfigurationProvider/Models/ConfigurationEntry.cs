namespace CatConsult.EnvConfigurationProvider.Models
{
    public class ConfigurationEntry
    {
        public ConfigurationEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}
