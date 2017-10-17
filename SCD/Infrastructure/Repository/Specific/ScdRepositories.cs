using AutoMapper;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Infrastructure.Mapping;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class ScdRepositories : IScdRepositories
    {
        public ScdRepositories(IMapper mapper)
        {
            ScdContext context = new ScdContext();

            UnitOfWork = new EFUnitOfWorkSpecific(context);

            ItensPlanoClassificacaoSpecific = new EFItemPlanoClassificacaoRepository(context.ItemPlanoClassificacao, mapper);
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IItemPlanoClassificacaoRepository ItensPlanoClassificacaoSpecific { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
