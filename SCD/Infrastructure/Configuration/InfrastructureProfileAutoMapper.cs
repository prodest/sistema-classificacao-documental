using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Persistence.Model;
using System.Collections.Generic;
using System.Linq;

namespace Prodest.Scd.Infrastructure.Configuration
{
    public class InfrastructureProfileAutoMapper : Profile
    {
        public InfrastructureProfileAutoMapper()
        {
            #region Critério de Restrição
            CreateMap<CriterioRestricao, CriterioRestricaoModel>().PreserveReferences()
                .ForMember(dest => dest.Grau, opt => opt.MapFrom(src => src.IdGrau))
                .ForMember(dest => dest.UnidadePrazoTermino, opt => opt.MapFrom(src => src.IdUnidadePrazoTermino))
            ;

            CreateMap<CriterioRestricaoModel, CriterioRestricao>().PreserveReferences()
                .ForMember(dest => dest.Id, opt =>
                 {
                     opt.Condition((src, dest, srcMember, destMember) =>
                     {
                         return (destMember == default(int));
                     });
                 })
                .ForMember(dest => dest.PlanoClassificacao, opt => opt.Ignore())
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao != null ? src.PlanoClassificacao.Id : default(int)))
                .ForMember(dest => dest.IdGrau, opt => opt.MapFrom(src => src.Grau)) 
                .ForMember(dest => dest.IdUnidadePrazoTermino, opt => opt.MapFrom(src => src.UnidadePrazoTermino))
                .ForMember(dest => dest.CriteriosRestricaoDocumento, opt => opt.Ignore())
                ;

            #endregion

            #region Documento
            CreateMap<Documento, DocumentoModel>()
                .PreserveReferences();

            CreateMap<DocumentoModel, Documento>().PreserveReferences()
                .ForMember(dest => dest.Id, opt =>
                    {
                        opt.Condition((src, dest, srcMember, destMember) =>
                        {
                            return (destMember == default(int));
                        });
                    })
                .ForMember(dest => dest.ItemPlanoClassificacao, opt => opt.Ignore())
                .ForMember(dest => dest.TipoDocumental, opt => opt.Ignore())
                .ForMember(dest => dest.IdItemPlanoClassificacao, opt => opt.MapFrom(src => src.ItemPlanoClassificacao != null ? src.ItemPlanoClassificacao.Id : default(int)))
                .ForMember(dest => dest.IdTipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental != null ? src.TipoDocumental.Id : default(int)));
            #endregion

            #region Item do Plano de Classificação
            CreateMap<ItemPlanoClassificacao, ItemPlanoClassificacaoModel>().PreserveReferences()
                ;
            CreateMap<ItemPlanoClassificacaoModel, ItemPlanoClassificacao>().PreserveReferences()
                .ForMember(dest => dest.Id, opt =>
                 {
                     opt.Condition((src, dest, srcMember, destMember) =>
                     {
                         return (destMember == default(int));
                     });
                 })
                .ForMember(dest => dest.NivelClassificacao, opt => opt.Ignore())
                .ForMember(dest => dest.PlanoClassificacao, opt => opt.Ignore())
                .ForMember(dest => dest.ItemPlanoClassificacaoParent, opt => opt.Ignore())
                .ForMember(dest => dest.IdNivelClassificacao, opt => opt.MapFrom(src => src.NivelClassificacao != null ? src.NivelClassificacao.Id : default(int)))
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao != null ? src.PlanoClassificacao.Id : default(int)))
                .ForMember(dest => dest.IdItemPlanoClassificacaoParent, opt => opt.MapFrom(src => src.ItemPlanoClassificacaoParent != null ? (int?)src.ItemPlanoClassificacaoParent.Id : null));
            #endregion

            #region Nível de Classificação
            CreateMap<NivelClassificacao, NivelClassificacaoModel>().PreserveReferences();

            CreateMap<NivelClassificacaoModel, NivelClassificacao>().PreserveReferences()
                .ForMember(dest => dest.Organizacao,
                opt =>
                {
                    opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        return (destMember == null);
                    });
                })
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(src => src.Organizacao != null ? src.Organizacao.Id : default(int)));
            #endregion

            #region Fundamento Legal
            CreateMap<FundamentoLegal, FundamentoLegalModel>().PreserveReferences();

            CreateMap<FundamentoLegalModel, FundamentoLegal>().PreserveReferences()
                .ForMember(dest => dest.Organizacao,
                opt =>
                {
                    opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        return (destMember == null);
                    });
                })
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(src => src.Organizacao != null ? src.Organizacao.Id : default(int)));
            #endregion

            #region Organização
            CreateMap<Organizacao, OrganizacaoModel>()
                .PreserveReferences()
                ;

            CreateMap<OrganizacaoModel, Organizacao>().PreserveReferences()
                .ForMember(dest => dest.PlanosClassificacao, opt => opt.Ignore());
            #endregion

            #region Plano de Classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModel>().PreserveReferences();

            CreateMap<PlanoClassificacaoModel, PlanoClassificacao>().PreserveReferences()
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        return (destMember == default(int));
                    });
                })
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(src => src.Organizacao != null ? src.Organizacao.Id : default(int)))
                .ForMember(dest => dest.Organizacao, opt => opt.Ignore())
                .ForMember(dest => dest.ItensPlanoClassificacao, opt => opt.Ignore());
            #endregion

            #region Temporalidade
            CreateMap<Temporalidade, TemporalidadeModel>().PreserveReferences()
                .ForMember(dest => dest.DestinacaoFinal, opt => opt.MapFrom(src => src.IdDestinacaoFinal))
                .ForMember(dest => dest.UnidadePrazoGuardaFaseCorrente, opt => opt.MapFrom(src => src.IdUnidadePrazoGuardaFaseCorrente))
                .ForMember(dest => dest.UnidadePrazoGuardaFaseIntermediaria, opt => opt.MapFrom(src => src.IdUnidadePrazoGuardaFaseIntermediaria));
                


            CreateMap<TemporalidadeModel, Temporalidade>().PreserveReferences()
                .ForMember(dest => dest.Id, opt =>
                 {
                     opt.Condition((src, dest, srcMember, destMember) =>
                     {
                         return (destMember == default(int));
                     });
                 })
                .ForMember(dest => dest.Documento, opt => opt.Ignore())
                .ForMember(dest => dest.IdDocumento, opt => opt.MapFrom(src => src.Documento != null ? src.Documento.Id : default(int)))
                .ForMember(dest => dest.IdDestinacaoFinal, opt => opt.MapFrom(src => src.DestinacaoFinal))
                .ForMember(dest => dest.IdUnidadePrazoGuardaFaseCorrente, opt => opt.MapFrom(src => src.UnidadePrazoGuardaFaseCorrente))
                .ForMember(dest => dest.IdUnidadePrazoGuardaFaseIntermediaria, opt => opt.MapFrom(src => src.UnidadePrazoGuardaFaseIntermediaria))
                ;
            #endregion

            #region TermoClassificacaoInformacao
            CreateMap<TermoClassificacaoInformacao, TermoClassificacaoInformacaoModel>()
                .PreserveReferences()
                .ForMember(dest => dest.GrauSigilo, opt => opt.MapFrom(src => src.IdGrauSigilo))
                .ForMember(dest => dest.TipoSigilo, opt => opt.MapFrom(src => src.IdTipoSigilo))
                .ForMember(dest => dest.UnidadePrazoSigilo, opt => opt.MapFrom(src => src.IdUnidadePrazoSigilo))
                ;

            CreateMap<TermoClassificacaoInformacaoModel, TermoClassificacaoInformacao>().PreserveReferences()
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        return (destMember == default(int));
                    });
                })
                .ForMember(dest => dest.Documento, opt => opt.Ignore())
                .ForMember(dest => dest.CriterioRestricao, opt => opt.Ignore())
                .ForMember(dest => dest.IdDocumento, opt => opt.MapFrom(src => src.Documento != null ? src.Documento.Id : default(int)))
                .ForMember(dest => dest.IdCriterioRestricao, opt => opt.MapFrom(src => src.CriterioRestricao != null ? src.CriterioRestricao.Id : default(int)))
                .ForMember(dest => dest.IdGrauSigilo, opt => opt.MapFrom(src => src.GrauSigilo))
                .ForMember(dest => dest.IdTipoSigilo, opt => opt.MapFrom(src => src.TipoSigilo))
                .ForMember(dest => dest.IdUnidadePrazoSigilo, opt => opt.MapFrom(src => src.UnidadePrazoSigilo))
                ;
            #endregion

            #region Tipo Documental
            CreateMap<TipoDocumental, TipoDocumentalModel>()
                .PreserveReferences()
                
                ;

            CreateMap<TipoDocumentalModel, TipoDocumental>().PreserveReferences()
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        return (destMember == default(int));
                    });
                })
                .ForMember(dest => dest.Organizacao, opt => opt.Ignore())
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(src => src.Organizacao != null ? src.Organizacao.Id : default(int)));
            #endregion
        }
    }
}