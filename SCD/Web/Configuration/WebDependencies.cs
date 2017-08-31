using Microsoft.Extensions.DependencyInjection;
using Prodest.Scd.Dependency;
using Prodest.Scd.Integration.Common.Base;
using System;
using System.Collections.Generic;

namespace Prodest.Scd.Web.Configuration
{
    public static class WebDependencies
    {
        public static void AddDependencies (IServiceCollection services)
        {
            Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>();
            dependencies = GetDependencies();

            foreach (var dep in dependencies)
            {
                services.AddScoped(dep.Key, dep.Value);
            }
        }

        public static Dictionary<Type, Type> GetDependencies()
        {
            Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>();

            dependencies = ProjectsDependencies.GetDependencies();

            dependencies.Add(typeof(IClientAccessTokenProvider), typeof(AcessoCidadaoClientAccessToken));

            return dependencies;
        }
    }
}