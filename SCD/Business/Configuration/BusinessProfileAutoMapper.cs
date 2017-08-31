using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Persistence.Model;

namespace Prodest.Scd.Business.Configuration
{
    public class BusinessProfileAutoMapper : Profile
    {

        public BusinessProfileAutoMapper()
        {
            #region Organização
            CreateMap<Organizacao, OrganizacaoModel>();

            CreateMap<OrganizacaoModel, Organizacao>()
                .ForMember(dest => dest.PlanosClassificacao, opt => opt.Ignore());
            #endregion

            #region Plano de Classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModel>()
                //.ForMember(dest => dest.GuidOrganizacao, opt => opt.Ignore())
                //.ForMember(dest => dest.Organizacao, opt => opt.Ignore())
                //.ForMember(dest => dest.ItensPlanoClassificacao, opt => opt.Ignore())
                ;

            CreateMap<PlanoClassificacaoModel, PlanoClassificacao>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion
        }
    }
}
