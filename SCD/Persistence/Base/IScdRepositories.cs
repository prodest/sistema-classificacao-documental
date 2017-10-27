using Prodest.Scd.Persistence.Model;
using System;

namespace Prodest.Scd.Persistence.Base
{
    public interface IScdRepositories : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IGenericRepository<ItemPlanoClassificacao> ItensPlanoClassificacao { get; }
        IGenericRepository<NivelClassificacao> NiveisClassificacao { get; }
        IGenericRepository<PlanoClassificacao> PlanosClassificacao { get; }
        IGenericRepository<Organizacao> Organizacoes { get; }
        IGenericRepository<TipoDocumental> TiposDocumentais { get; }

    }
}
