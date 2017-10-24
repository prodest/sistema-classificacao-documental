using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Infrastructure.Mapping;
using Prodest.Scd.Persistence.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFOrganizacaoRepository : IOrganizacaoRepository
    {
        private ScdContext _context;
        private DbSet<Organizacao> _set;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFOrganizacaoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.Organizacao;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrganizacaoModel> SearchByGuidAsync(Guid guidOrganizacao)
        {
            Organizacao organizacao = await SearchPersistenceAsync(guidOrganizacao);

            OrganizacaoModel organizacaoModel = _mapper.Map<OrganizacaoModel>(organizacao);

            return organizacaoModel;
        }

        private async Task<Organizacao> SearchPersistenceAsync(Guid guidOrganizacao)
        {
            Organizacao organizacao = await _set.Where(o => o.GuidOrganizacao.Equals(guidOrganizacao))
                                                .SingleOrDefaultAsync();

            return organizacao;
        }
    }
}
