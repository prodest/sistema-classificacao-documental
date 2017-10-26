using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Presentation.ViewModel;

namespace Prodest.Scd.Presentation.Configuration
{
    class PresentationProfileAutoMapper : Profile
    {
        public PresentationProfileAutoMapper()
        {
            CreateMap<PlanoClassificacaoModel, PlanoClassificacaoEntidade>().ReverseMap();
            CreateMap<ItemPlanoClassificacaoModel, ItemPlanoClassificacaoEntidade>().ReverseMap();
            //    .ForMember(dest => dest.IdItemPlanoClassificacaoParent, opt => opt.MapFrom(src => src.ItemPlanoClassificacaoParent != null ? (int?)src.ItemPlanoClassificacaoParent.Id : null));
            //CreateMap<ItemPlanoClassificacaoEntidade, ItemPlanoClassificacaoModel>()
            //    .ForMember(dest => dest.ItemPlanoClassificacaoParent, opt => opt.MapFrom(src => src.IdItemPlanoClassificacaoParent.HasValue ?  new ItemPlanoClassificacaoModel { Id = src.IdItemPlanoClassificacaoParent.Value } : null));
            CreateMap<NivelClassificacaoModel, NivelClassificacaoEntidade>().ReverseMap();

        }
    }
}
