using AutoMapper;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Presentation.ViewModel;

namespace Prodest.Scd.Presentation.Configuration
{
    class PresentationProfileAutoMapper : Profile
    {
        public PresentationProfileAutoMapper()
        {
            CreateMap<TermoClassificacaoInformacaoModel, TermoClassificacaoInformacaoEntidade>().ReverseMap();
            CreateMap<ItemPlanoClassificacaoModel, ItemPlanoClassificacaoEntidade>().ReverseMap();
            CreateMap<NivelClassificacaoModel, NivelClassificacaoEntidade>().ReverseMap();
            CreateMap<TipoDocumentalModel, TipoDocumentalEntidade>().ReverseMap();
            CreateMap<FundamentoLegalModel, FundamentoLegalEntidade>().ReverseMap();
            CreateMap<TemporalidadeModel, TemporalidadeEntidade>().ReverseMap();

            CreateMap<PlanoClassificacaoModel, PlanoClassificacaoEntidade>().MaxDepth(1).ReverseMap();
            CreateMap<CriterioRestricaoModel, CriterioRestricaoEntidade>().MaxDepth(1).ReverseMap();
            //CreateMap<ItemPlanoClassificacaoModel, ItemPlanoClassificacaoEntidade>().MaxDepth(1).ReverseMap();
            //CreateMap<NivelClassificacaoModel, NivelClassificacaoEntidade>().MaxDepth(1).ReverseMap();
            //CreateMap<TipoDocumentalModel, TipoDocumentalEntidade>().MaxDepth(1).ReverseMap();
            CreateMap<DocumentoModel, DocumentoEntidade>().MaxDepth(1).ReverseMap();
        }
    }
}
