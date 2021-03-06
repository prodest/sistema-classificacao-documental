﻿using Prodest.Scd.Business;
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
            dependencies.Add(typeof(ICriterioRestricaoCore), typeof(CriterioRestricaoCore));
            dependencies.Add(typeof(IDocumentoCore), typeof(DocumentoCore));
            dependencies.Add(typeof(IFundamentoLegalCore), typeof(FundamentoLegalCore));
            dependencies.Add(typeof(IItemPlanoClassificacaoCore), typeof(ItemPlanoClassificacaoCore));
            dependencies.Add(typeof(INivelClassificacaoCore), typeof(NivelClassificacaoCore));
            dependencies.Add(typeof(IOrganizacaoCore), typeof(OrganizacaoCore));
            dependencies.Add(typeof(IPlanoClassificacaoCore), typeof(PlanoClassificacaoCore));
            dependencies.Add(typeof(ITermoClassificacaoInformacaoCore), typeof(TermoClassificacaoInformacaoCore));

            dependencies.Add(typeof(ITemporalidadeCore), typeof(TemporalidadeCore));
            dependencies.Add(typeof(ITipoDocumentalCore), typeof(TipoDocumentalCore));
            #endregion

            #region Validation
            dependencies.Add(typeof(CriterioRestricaoValidation), typeof(CriterioRestricaoValidation));
            dependencies.Add(typeof(DocumentoValidation), typeof(DocumentoValidation));
            dependencies.Add(typeof(FundamentoLegalValidation), typeof(FundamentoLegalValidation));
            dependencies.Add(typeof(OrganizacaoValidation), typeof(OrganizacaoValidation));
            dependencies.Add(typeof(PlanoClassificacaoValidation), typeof(PlanoClassificacaoValidation));
            dependencies.Add(typeof(TermoClassificacaoInformacaoValidation), typeof(TermoClassificacaoInformacaoValidation));
            dependencies.Add(typeof(ItemPlanoClassificacaoValidation), typeof(ItemPlanoClassificacaoValidation));
            dependencies.Add(typeof(NivelClassificacaoValidation), typeof(NivelClassificacaoValidation));
            dependencies.Add(typeof(TemporalidadeValidation), typeof(TemporalidadeValidation));
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
            dependencies.Add(typeof(ITermoClassificacaoInformacaoService), typeof(TermoClassificacaoInformacaoService));
            dependencies.Add(typeof(IFundamentoLegalService), typeof(FundamentoLegalService));
            dependencies.Add(typeof(IItemPlanoClassificacaoService), typeof(ItemPlanoClassificacaoService));
            dependencies.Add(typeof(INivelClassificacaoService), typeof(NivelClassificacaoService));
            dependencies.Add(typeof(ITipoDocumentalService), typeof(TipoDocumentalService));
            dependencies.Add(typeof(IDocumentoService), typeof(DocumentoService));
            dependencies.Add(typeof(ITemporalidadeService), typeof(TemporalidadeService));
            dependencies.Add(typeof(ICriterioRestricaoService), typeof(CriterioRestricaoService));
            #endregion

            #region Infrastructure
            dependencies.Add(typeof(ICriterioRestricaoRepository), typeof(EFCriterioRestricaoRepository));
            dependencies.Add(typeof(IDocumentoRepository), typeof(EFDocumentoRepository));
            dependencies.Add(typeof(IFundamentoLegalRepository), typeof(EFFundamentoLegalRepository));
            dependencies.Add(typeof(IItemPlanoClassificacaoRepository), typeof(EFItemPlanoClassificacaoRepository));
            dependencies.Add(typeof(IPlanoClassificacaoRepository), typeof(EFPlanoClassificacaoRepository));
            dependencies.Add(typeof(ITermoClassificacaoInformacaoRepository), typeof(EFTermoClassificacaoInformacaoRepository));
            dependencies.Add(typeof(INivelClassificacaoRepository), typeof(EFNivelClassificacaoRepository));
            dependencies.Add(typeof(IOrganizacaoRepository), typeof(EFOrganizacaoRepository));
            dependencies.Add(typeof(ITemporalidadeRepository), typeof(EFTemporalidadeRepository));
            dependencies.Add(typeof(ITipoDocumentalRepository), typeof(EFTipoDocumentalRepository));
            dependencies.Add(typeof(ScdContext), typeof(ScdContext));
            dependencies.Add(typeof(IUnitOfWork), typeof(EFUnitOfWorkSpecific));
            #endregion

            return dependencies;
        }
    }
}
