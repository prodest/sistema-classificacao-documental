using AutoMapper;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Infrastructure.Mapping;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFScdRepositories : IScdRepositories
    {
        public EFScdRepositories(IMapper mapper)
        {
            ScdContext context = new ScdContext();

            UnitOfWork = new EFUnitOfWorkSpecific(context);

            DocumentosSpecific = new EFDocumentoRepository(context, mapper, UnitOfWork);

            ItensPlanoClassificacaoSpecific = new EFItemPlanoClassificacaoRepository(context, mapper, UnitOfWork);

            NiveisClassificacaoSpecific = new EFNivelClassificacaoRepository(context, mapper, UnitOfWork);

            OrganizacoesSpecific = new EFOrganizacaoRepository(context, mapper, UnitOfWork);

            PlanosClassificacaoSpecific = new EFPlanoClassificacaoRepository(context, mapper, UnitOfWork);

            CriteriosRestricaoSpecific = new EFCriterioRestricaoRepository(context, mapper, UnitOfWork);

            TemporalidadesSpecific = new EFTemporalidadeRepository(context, mapper, UnitOfWork);

            TiposDocumentaisSpecific = new EFTipoDocumentalRepository(context, mapper, UnitOfWork);            
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public ICriterioRestricaoRepository CriteriosRestricaoSpecific { get; private set; }

        public IDocumentoRepository DocumentosSpecific { get; private set; }

        public IItemPlanoClassificacaoRepository ItensPlanoClassificacaoSpecific { get; private set; }

        public INivelClassificacaoRepository NiveisClassificacaoSpecific { get; private set; }

        public IOrganizacaoRepository OrganizacoesSpecific { get; private set; }

        public IPlanoClassificacaoRepository PlanosClassificacaoSpecific { get; private set; }

        public ITemporalidadeRepository TemporalidadesSpecific { get; private set; }

        public ITipoDocumentalRepository TiposDocumentaisSpecific { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
