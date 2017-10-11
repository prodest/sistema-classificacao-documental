using Prodest.Scd.Business;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.Infrastructure.Repository;
using Prodest.Scd.Integration.Organograma.Base;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Presentation;
using Prodest.Scd.Presentation.Base;
using System;
using System.Collections.Generic;

namespace Prodest.Scd.Dependency
{
    public class ProjectsDependencies
    {
        public static Dictionary<Type, Type> GetDependencies()
        {
            Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>();

            #region Business
            #region Core
            dependencies.Add(typeof(IPlanoClassificacaoCore), typeof(PlanoClassificacaoCore));
            dependencies.Add(typeof(IItemPlanoClassificacaoCore), typeof(ItemPlanoClassificacaoCore));
            dependencies.Add(typeof(INivelClassificacaoCore), typeof(NivelClassificacaoCore));
            dependencies.Add(typeof(IOrganizacaoCore), typeof(OrganizacaoCore));
            #endregion

            #region Validation
            dependencies.Add(typeof(OrganizacaoValidation), typeof(OrganizacaoValidation));
            dependencies.Add(typeof(PlanoClassificacaoValidation), typeof(PlanoClassificacaoValidation));
            dependencies.Add(typeof(ItemPlanoClassificacaoValidation), typeof(ItemPlanoClassificacaoValidation));
            dependencies.Add(typeof(NivelClassificacaoValidation), typeof(NivelClassificacaoValidation));
            #endregion
            #endregion

            #region Integration
            dependencies.Add(typeof(IOrganogramaService), typeof(OrganogramaService));
            #endregion

            #region Persistence
            dependencies.Add(typeof(IScdRepositories), typeof(ScdRepositories));
            #endregion

            #region Presentation
            dependencies.Add(typeof(IPlanoClassificacaoService), typeof(PlanoClassificacaoService));
            dependencies.Add(typeof(IItemPlanoClassificacaoService), typeof(ItemPlanoClassificacaoService));
            dependencies.Add(typeof(INivelClassificacaoService), typeof(NivelClassificacaoService));
            #endregion

            return dependencies;
        }
    }
}
