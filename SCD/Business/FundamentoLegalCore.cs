using AutoMapper;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class FundamentoLegalCore : IFundamentoLegalCore
    {
        private IUnitOfWork _unitOfWork;
        private IFundamentoLegalRepository _fundamentosLegais;
        private FundamentoLegalValidation _validation;
        private IOrganizacaoCore _organizacaoCore;

        public FundamentoLegalCore(IScdRepositories repositories, FundamentoLegalValidation validation, IOrganizacaoCore organizacaoCore)
        {
            _unitOfWork = repositories.UnitOfWork;
            _fundamentosLegais = repositories.FundamentosLegaisSpecific;
            _validation = validation;
            _organizacaoCore = organizacaoCore;
        }

        public async Task<int> CountAsync(Guid guidOrganizacao)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            var count = await _fundamentosLegais.CountByOrganizacaoAsync(guidOrganizacao);

            return count;
        }

        public async Task DeleteAsync(int id)
        {
            FundamentoLegalModel fundamentoLegalModel = await SearchAsync(id);

            _validation.CanDelete(fundamentoLegalModel);

            await _fundamentosLegais.RemoveAsync(fundamentoLegalModel.Id);
        }

        public async Task<FundamentoLegalModel> InsertAsync(FundamentoLegalModel fundamentoLegalModel)
        {
            _validation.BasicValid(fundamentoLegalModel);

            _validation.IdInsertValid(fundamentoLegalModel.Id);

            fundamentoLegalModel.Organizacao = await GetOrganizacao();

            //fundamentoLegalModel.Ativo = true;

            fundamentoLegalModel = await _fundamentosLegais.AddAsync(fundamentoLegalModel);

            return fundamentoLegalModel;
        }

        public async Task<FundamentoLegalModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            FundamentoLegalModel fundamentoLegalModel = await _fundamentosLegais.SearchAsync(id);

            _validation.Found(fundamentoLegalModel);

            return fundamentoLegalModel;
        }

        public async Task<ICollection<FundamentoLegalModel>> SearchAsync(Guid guidOrganizacao, int page, int count)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            _validation.PaginationSearch(page, count);

            ICollection<FundamentoLegalModel> fundamentosLegaisModel = await _fundamentosLegais.SearchByOrganizacaoAsync(guidOrganizacao, page, count);

            return fundamentosLegaisModel;
        }

        public async Task UpdateAsync(FundamentoLegalModel fundamentoLegalModel)
        {
            _validation.Valid(fundamentoLegalModel);

            FundamentoLegalModel fundamentoLegalModelOld = await SearchAsync(fundamentoLegalModel.Id);

            _validation.CanUpdate(fundamentoLegalModel, fundamentoLegalModelOld);

            fundamentoLegalModel.Organizacao = await GetOrganizacao();

            await _fundamentosLegais.UpdateAsync(fundamentoLegalModel);
        }

        private async Task<OrganizacaoModel> GetOrganizacao()
        {
            //TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            Guid guidProdest = new Guid(Environment.GetEnvironmentVariable("guidGEES"));

            OrganizacaoModel organizacaoModel = await _organizacaoCore.SearchAsync(guidProdest);

            return organizacaoModel;
        }
    }
}