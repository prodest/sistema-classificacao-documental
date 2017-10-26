using Prodest.Scd.Business;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.Infrastructure.Mapping;
using Prodest.Scd.Infrastructure.Repository.Specific;
using Prodest.Scd.Integration.Organograma.Base;
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
            dependencies.Add(typeof(ITipoDocumentalCore), typeof(TipoDocumentalCore));
            dependencies.Add(typeof(IOrganizacaoCore), typeof(OrganizacaoCore));
            #endregion

            #region Validation
            dependencies.Add(typeof(OrganizacaoValidation), typeof(OrganizacaoValidation));
            dependencies.Add(typeof(PlanoClassificacaoValidation), typeof(PlanoClassificacaoValidation));
            dependencies.Add(typeof(ItemPlanoClassificacaoValidation), typeof(ItemPlanoClassificacaoValidation));
            dependencies.Add(typeof(NivelClassificacaoValidation), typeof(NivelClassificacaoValidation));
            dependencies.Add(typeof(TipoDocumentalValidation), typeof(TipoDocumentalValidation));
            #endregion
            #endregion

            #region Integration
            dependencies.Add(typeof(IOrganogramaService), typeof(OrganogramaService));
            #endregion

            #region Persistence
            dependencies.Add(typeof(IScdRepositories), typeof(EFScdRepositories));
            #endregion

            #region Presentation
            dependencies.Add(typeof(IPlanoClassificacaoService), typeof(PlanoClassificacaoService));
            dependencies.Add(typeof(IItemPlanoClassificacaoService), typeof(ItemPlanoClassificacaoService));
            dependencies.Add(typeof(INivelClassificacaoService), typeof(NivelClassificacaoService));
            dependencies.Add(typeof(ITipoDocumentalService), typeof(TipoDocumentalService));
            #endregion

            #region Infrastructure
            dependencies.Add(typeof(IItemPlanoClassificacaoRepository), typeof(EFItemPlanoClassificacaoRepository));
            dependencies.Add(typeof(IPlanoClassificacaoRepository), typeof(EFPlanoClassificacaoRepository));
            dependencies.Add(typeof(INivelClassificacaoRepository), typeof(EFNivelClassificacaoRepository));
            dependencies.Add(typeof(IOrganizacaoRepository), typeof(EFOrganizacaoRepository));
            dependencies.Add(typeof(ITipoDocumentalRepository), typeof(EFTipoDocumentalRepository));
            dependencies.Add(typeof(ScdContext), typeof(ScdContext));
            dependencies.Add(typeof(IUnitOfWork), typeof(EFUnitOfWorkSpecific));

            #endregion

            return dependencies;
        }
    }
}
