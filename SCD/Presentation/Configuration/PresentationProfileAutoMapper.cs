using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Presentation.ViewModel;

namespace Prodest.Scd.Presentation.Configuration
{
    class PresentationProfileAutoMapper : Profile
    {
        public PresentationProfileAutoMapper()
        {
            //CreateMap<PlanoClassificacaoModel, PlanoClassificacaoEntidade>().ReverseMap();
            CreateMap<ItemPlanoClassificacaoModel, ItemPlanoClassificacaoEntidade>().ReverseMap();
            CreateMap<NivelClassificacaoModel, NivelClassificacaoEntidade>().ReverseMap();
            CreateMap<TipoDocumentalModel, TipoDocumentalEntidade>().ReverseMap();
            //CreateMap<DocumentoModel, DocumentoEntidade>().ReverseMap();
            CreateMap<SigiloModel, SigiloEntidade>().ReverseMap();

            CreateMap<PlanoClassificacaoModel, PlanoClassificacaoEntidade>().MaxDepth(1).ReverseMap();
            //CreateMap<ItemPlanoClassificacaoModel, ItemPlanoClassificacaoEntidade>().MaxDepth(1).ReverseMap();
            //CreateMap<NivelClassificacaoModel, NivelClassificacaoEntidade>().MaxDepth(1).ReverseMap();
            //CreateMap<TipoDocumentalModel, TipoDocumentalEntidade>().MaxDepth(1).ReverseMap();
            CreateMap<DocumentoModel, DocumentoEntidade>().MaxDepth(1).ReverseMap();
        }
    }
}
