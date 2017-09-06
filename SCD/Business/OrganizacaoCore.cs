using AutoMapper;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System;
using System.Linq;

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

        public OrganizacaoModel SearchAsync(Guid guidOrganizacao)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            Organizacao organizacao = _organizacoes.Where(o => o.GuidOrganizacao.Equals(guidOrganizacao))
                                                   .SingleOrDefault();

            _validation.Found(organizacao);

            OrganizacaoModel organizacaoModel = _mapper.Map<OrganizacaoModel>(organizacao);

            return organizacaoModel;
        }

        public OrganizacaoModel SearchAsync(int id, Guid guidOrganizacao)
        {
            _validation.IdValid(id);
            _validation.OrganizacaoValid(guidOrganizacao);

            Organizacao organizacao = _organizacoes.Where(o => o.Id == id
                                                            && o.GuidOrganizacao.Equals(guidOrganizacao))
                                                   .SingleOrDefault();

            _validation.Found(organizacao);

            OrganizacaoModel organizacaoModel = _mapper.Map<OrganizacaoModel>(organizacao);

            return organizacaoModel;
        }
    }
}
