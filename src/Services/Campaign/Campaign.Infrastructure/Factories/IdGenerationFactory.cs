using System;

namespace Campaign.Infrastructure.Factories
{
    public static class IdGenerationFactory 
    {
        /// <summary>
        /// Generates unique key depends on Infrastructure
        /// </summary>
        /// <returns></returns>
        public static Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}