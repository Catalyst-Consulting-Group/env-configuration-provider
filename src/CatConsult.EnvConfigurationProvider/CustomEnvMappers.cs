using System.Collections.Generic;
using CatConsult.EnvConfigurationProvider.Models;

namespace CatConsult.EnvConfigurationProvider
{
    /// <summary>
    /// Performs a custom environment variable mapping operation and returns a single <see cref="ConfigurationEntry"/> result
    /// </summary>
    public delegate ConfigurationEntry CustomEnvMapper(IReadOnlyDictionary<string, string> envs);

    /// <summary>
    /// Performs a custom environment variable mapping operation and returns a collection of <see cref="ConfigurationEntry"/> results
    /// </summary>
    public delegate IEnumerable<ConfigurationEntry> CustomEnvMultiMapper(IReadOnlyDictionary<string, string> envs);
}
