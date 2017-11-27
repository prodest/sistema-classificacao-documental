using System;

namespace Prodest.Scd.Business.Repository.Base
{
    public interface IScdRepositories : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        ICriterioRestricaoRepository CriteriosRestricaoSpecific { get; }

        IDocumentoRepository DocumentosSpecific { get; }

        IItemPlanoClassificacaoRepository ItensPlanoClassificacaoSpecific { get; }

        INivelClassificacaoRepository NiveisClassificacaoSpecific { get; }

        IOrganizacaoRepository OrganizacoesSpecific { get; }

        IPlanoClassificacaoRepository PlanosClassificacaoSpecific { get; }

        ITemporalidadeRepository TemporalidadesSpecific { get; }

        ITermoClassificacaoInformacaoRepository TermosClassificacaoInformacaoSpecific { get; }

        ITipoDocumentalRepository TiposDocumentaisSpecific { get; }
    }
}
