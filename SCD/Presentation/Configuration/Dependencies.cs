using Prodest.Scd.Business;
using Prodest.Scd.Business.Base;
using System;
using System.Collections.Generic;

namespace Prodest.Scd.Presentation.Configuration
{
    public static class Dependencies
    {
        public static Dictionary<Type, Type> GetDependencies()
        {
            Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>();

            dependencies = Business.Configuration.Dependencies.GetDependencies();
            dependencies.Add(typeof(IPlanoClassificacaoCore), typeof(PlanoClassificacaoCore));
            dependencies.Add(typeof(IOrganizacaoCore), typeof(OrganizacaoCore));

            return dependencies;
        }
    }
}
