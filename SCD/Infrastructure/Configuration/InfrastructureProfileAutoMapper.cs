﻿using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Persistence.Model;

namespace Prodest.Scd.Infrastructure.Configuration
{
    public class InfrastructureProfileAutoMapper : Profile
    {
        public InfrastructureProfileAutoMapper()
        {
            #region Documento
            CreateMap<Documento, DocumentoModel>()
                .MaxDepth(1);

            CreateMap<DocumentoModel, Documento>()
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
            CreateMap<ItemPlanoClassificacao, ItemPlanoClassificacaoModel>()
                .MaxDepth(1);
            CreateMap<ItemPlanoClassificacaoModel, ItemPlanoClassificacao>()
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
            CreateMap<NivelClassificacao, NivelClassificacaoModel>().MaxDepth(1);

            CreateMap<NivelClassificacaoModel, NivelClassificacao>()
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
            CreateMap<Organizacao, OrganizacaoModel>().MaxDepth(1);

            CreateMap<OrganizacaoModel, Organizacao>()
                .ForMember(dest => dest.PlanosClassificacao, opt => opt.Ignore());
            #endregion

            #region Plano de Classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModel>()
                .MaxDepth(1)
            ;

            CreateMap<PlanoClassificacaoModel, PlanoClassificacao>()
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

            #region Sigilo
            CreateMap<Sigilo, SigiloModel>().ReverseMap()
                .MaxDepth(1);
            #endregion

            #region Tipo Documental
            CreateMap<TipoDocumental, TipoDocumentalModel>()
                .MaxDepth(1);

            CreateMap<TipoDocumentalModel, TipoDocumental>()
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
