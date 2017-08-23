using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Persistence.Base;
using System;
using System.Collections.Generic;

namespace Prodest.Scd.Business.Configuration
{
    public static class Dependencies
    {
        public static Dictionary<Type, Type> GetDependencies()
        {
            Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>();

            dependencies.Add(typeof(IScdRepositories), typeof(ScdRepositories));
            
            return dependencies;
        }
    }
}