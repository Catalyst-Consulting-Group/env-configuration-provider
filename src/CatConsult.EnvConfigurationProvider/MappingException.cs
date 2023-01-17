using System;

namespace CatConsult.EnvConfigurationProvider
{
    public class MappingException : Exception
    {
        public MappingException()
        {
        }

        public MappingException(string message) : base(message)
        {
        }

        public MappingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
