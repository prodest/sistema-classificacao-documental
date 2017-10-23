using System;

namespace Prodest.Scd.Business.Repository.Base
{
    public interface IScdRepositories : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IItemPlanoClassificacaoRepository ItensPlanoClassificacaoSpecific { get; }

        INivelClassificacaoRepository NiveisClassificacaoSpecific { get; }

        IOrganizacaoRepository OrganizacoesSpecific { get; }

        IPlanoClassificacaoRepository PlanosClassificacaoSpecific { get; }

        ITipoDocumentalRepository TiposDocumentaisSpecific { get; }
    }
}
