using Prodest.Scd.Infrastructure.Mapping;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;

namespace Prodest.Scd.Infrastructure.Repository
{
    public class ScdRepositories : IScdRepositories
    {
        public ScdRepositories()
        {
            UnitOfWork = new EFUnitOfWork(new ScdContext());

            ItensPlanoClassificacao = UnitOfWork.MakeGenericRepository<ItemPlanoClassificacao>();
            NiveisClassificacao = UnitOfWork.MakeGenericRepository<NivelClassificacao>();
            PlanosClassificacao = UnitOfWork.MakeGenericRepository<PlanoClassificacao>();
            Organizacoes = UnitOfWork.MakeGenericRepository<Organizacao>();
            TiposDocumentais = UnitOfWork.MakeGenericRepository<TipoDocumental>();
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IGenericRepository<ItemPlanoClassificacao> ItensPlanoClassificacao { get; private set; }

        public IGenericRepository<NivelClassificacao> NiveisClassificacao { get; private set; }

        public IGenericRepository<PlanoClassificacao> PlanosClassificacao { get; private set; }

        public IGenericRepository<Organizacao> Organizacoes { get; private set; }
        public IGenericRepository<TipoDocumental> TiposDocumentais { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
