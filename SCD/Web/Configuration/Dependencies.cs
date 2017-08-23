using Microsoft.Extensions.DependencyInjection;
using Prodest.Scd.Presentation;
using Prodest.Scd.Presentation.Base;
using System;
using System.Collections.Generic;

namespace Prodest.Scd.Web.Configuration
{
    public static class Dependencies
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

            dependencies = Presentation.Configuration.Dependencies.GetDependencies();

            dependencies.Add(typeof(IPlanoClassificacaoService), typeof(PlanoClassificacaoService));

            return dependencies;
        }

    }
}
