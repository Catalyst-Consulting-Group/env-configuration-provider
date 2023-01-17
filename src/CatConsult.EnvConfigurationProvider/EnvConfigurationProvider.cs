using System.Collections.Generic;
using DotNetEnv;
using Microsoft.Extensions.Configuration;

namespace CatConsult.EnvConfigurationProvider
{
    public class EnvConfigurationProvider : ConfigurationProvider
    {
        public List<EnvMapping> Mappings { get; } = new List<EnvMapping>();

        public override void Load()
        {
            var envs = Env.TraversePath().Load().ToDictionary();

            foreach (var mapping in Mappings)
            {
                var found = envs.TryGetValue(mapping.Env, out var value);

                if (mapping.IsRequired && !found)
                {
                    throw new MappingException($"Environment Key: '{mapping.Env}' was not found");
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    value = mapping.DefaultValue;
                }

                Data[mapping.ConfigurationKey] = value;
            }
        }
    }
}
