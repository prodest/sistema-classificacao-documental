using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class OrganizacaoCore : IOrganizacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private IOrganizacaoRepository _organizacoes;
        private OrganizacaoValidation _validation;

        public OrganizacaoCore(IScdRepositories repositories, OrganizacaoValidation validation)
        {
            _unitOfWork = repositories.UnitOfWork;
            _organizacoes = repositories.OrganizacoesSpecific;
            _validation = validation;
        }

        public async Task<OrganizacaoModel> SearchAsync(Guid guidOrganizacao)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            OrganizacaoModel organizacaoModel = await _organizacoes.SearchByGuidAsync(guidOrganizacao);

            _validation.Found(organizacaoModel);

            return organizacaoModel;
        }
    }
}
