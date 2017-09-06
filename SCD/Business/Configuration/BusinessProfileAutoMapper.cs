using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Persistence.Model;
using System;

namespace Prodest.Scd.Business.Configuration
{
    public class BusinessProfileAutoMapper : Profile
    {

        public BusinessProfileAutoMapper()
        {
            #region Item do Plano de Classificação
            CreateMap<ItemPlanoClassificacao, ItemPlanoClassificacaoModel>().ReverseMap();
            #endregion

            #region Nível de Classificação
            CreateMap<NivelClassificacao, NivelClassificacaoModel>().ReverseMap();
            #endregion

            #region Organização
            CreateMap<Organizacao, OrganizacaoModel>();

            CreateMap<OrganizacaoModel, Organizacao>()
                .ForMember(dest => dest.PlanosClassificacao, opt => opt.Ignore());
            #endregion

            #region Plano de Classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModel>()
                .MaxDepth(1)
            ;

            CreateMap<PlanoClassificacaoModel, PlanoClassificacao>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForAllMembers(opt =>
                //{
                //    opt.Condition((src, dest, srcMember, destMember) =>
                //    {
                //        bool mapear = false;
                //        if (destMember == null)
                //            mapear = true;
                //        else if (typeof(Guid).Equals(destMember.GetType()))
                //        {
                //            Guid guid = (Guid)destMember;
                //            if (guid.Equals(Guid.Empty))
                //                mapear = true;
                //        }
                //        else
                //        {
                //            try
                //            {
                //                int valor = (int)destMember;
                //                if (valor == 0)
                //                    mapear = true;
                //            }
                //            catch (Exception)
                //            { }
                //        }
                //        return mapear;
                //    });
                //});
            ;
            #endregion
        }
    }
}
