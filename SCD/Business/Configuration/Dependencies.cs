using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Integration.Organograma;
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

            dependencies.Add(typeof(PlanoClassificacaoValidation), typeof(PlanoClassificacaoValidation));
            dependencies.Add(typeof(OrganizacaoValidation), typeof(OrganizacaoValidation));

            dependencies.Add(typeof(OrganogramaService), typeof(OrganogramaService));

            return dependencies;
        }
    }
}