using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Persistence.Model;

namespace Prodest.Scd.Business.Configuration
{
    public class ProfileAutoMapper : Profile
    {

        public ProfileAutoMapper()
        {
            #region Organização
            CreateMap<Organizacao, OrganizacaoModel>()
                .ReverseMap();
            #endregion

            #region Plano de Classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModel>()
                .ReverseMap();
            #endregion
        }
    }
}
