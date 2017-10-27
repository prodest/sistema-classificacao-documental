using System;

namespace Prodest.Scd.Business.Repository.Base
{
    public interface IScdRepositories : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IDocumentoRepository DocumentosSpecific { get; }

        IItemPlanoClassificacaoRepository ItensPlanoClassificacaoSpecific { get; }

        INivelClassificacaoRepository NiveisClassificacaoSpecific { get; }

        IOrganizacaoRepository OrganizacoesSpecific { get; }

        IPlanoClassificacaoRepository PlanosClassificacaoSpecific { get; }

        ITipoDocumentalRepository TiposDocumentaisSpecific { get; }

        ISigiloRepository SigilosSpecific { get; }
    }
}
