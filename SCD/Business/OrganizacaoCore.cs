using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class OrganizacaoCore : IOrganizacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Organizacao> _organizacoes;
        private OrganizacaoValidation _validation;
        private IMapper _mapper;

        public OrganizacaoCore(IScdRepositories repositories, OrganizacaoValidation validation, IMapper mapper)
        {
            _unitOfWork = repositories.UnitOfWork;
            _organizacoes = repositories.Organizacoes;
            _validation = validation;
            _mapper = mapper;
        }

        public async Task<OrganizacaoModel> SearchAsync(string guid)
        {
            _validation.OrganizacaoFilled(guid);
            _validation.OrganizacaoValid(guid);

            Guid g = new Guid(guid);

            Organizacao organizacao = await _organizacoes.Where(o => o.GuidOrganizacao.Equals(g))
                                                         .SingleOrDefaultAsync();

            _validation.Found(organizacao);

            OrganizacaoModel organizacaoModel = _mapper.Map<OrganizacaoModel>(organizacao);

            return organizacaoModel;
        }
    }
}
