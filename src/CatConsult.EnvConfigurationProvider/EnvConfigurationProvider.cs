using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CatConsult.EnvConfigurationProvider.Models;
using DotNetEnv;
using Microsoft.Extensions.Configuration;

namespace CatConsult.EnvConfigurationProvider
{
    public class EnvConfigurationProvider : ConfigurationProvider
    {
        private readonly List<EnvMapping> _envMappings = new List<EnvMapping>();
        private readonly List<CustomEnvMapper> _customEnvMappers = new List<CustomEnvMapper>();
        private readonly List<CustomEnvMultiMapper> _customEnvMultiMappers = new List<CustomEnvMultiMapper>();

        public void AddMapping(EnvMapping mapping) => _envMappings.Add(mapping);

        public void AddCustomMapper(CustomEnvMapper mapper) => _customEnvMappers.Add(mapper);

        public void AddCustomMultiMapper(CustomEnvMultiMapper mapper) => _customEnvMultiMappers.Add(mapper);

        public override void Load()
        {
            var envs = Env
                .NoClobber()
                .NoEnvVars()
                .TraversePath()
                .Load()
                .ToDictionary();

            var systemEnvs = Environment.GetEnvironmentVariables()
                .Cast<DictionaryEntry>()
                .Select(e => new KeyValuePair<string, string>((string)e.Key, (string)e.Value));

            foreach (var env in systemEnvs)
            {
                envs[env.Key] = env.Value;
            }
            
            ProcessEnvMappings(envs);
            ProcessCustomEnvMappers(envs);
            ProcessCustomEnvMultiMappers(envs);
        }

        private void ProcessEnvMappings(IReadOnlyDictionary<string, string> envs)
        {
            foreach (var mapping in _envMappings)
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

        private void ProcessCustomEnvMappers(IReadOnlyDictionary<string, string> envs)
        {
            foreach (var mapper in _customEnvMappers)
            {
                AddEntry(mapper(envs));
            }
        }

        private void AddEntry(ConfigurationEntry entry) => Data[entry.Key] = entry.Value;

        private void ProcessCustomEnvMultiMappers(IReadOnlyDictionary<string, string> envs)
        {
            foreach (var mapper in _customEnvMultiMappers)
            {
                var entries = mapper(envs);

                foreach (var entry in entries)
                {
                    AddEntry(entry);
                }
            }
        }
    }
}
