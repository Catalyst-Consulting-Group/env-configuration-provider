namespace CatConsult.EnvConfigurationProvider.Models
{
    /// <summary>
    /// Encapsulates a single environment variable to configuration key mapping
    /// </summary>
    public class EnvMapping
    {
        /// <summary>
        /// The key or name of the environment variable
        /// </summary>
        public string Env { get; set; }

        /// <summary>
        /// The configuration key to map the value of <see cref="Env"/> to
        /// </summary>
        public string ConfigurationKey { get; set; }

        /// <summary>
        /// Controls whether or not to throw a <see cref="MappingException"/> if the environment variable is not found
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// An default value that will be supplied for an optional environment variable that is not found
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
