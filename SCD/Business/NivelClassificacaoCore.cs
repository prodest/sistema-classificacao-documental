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
    public class NivelClassificacaoCore : INivelClassificacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private INivelClassificacaoRepository _niveisClassificacao;
        private NivelClassificacaoValidation _validation;
        private IOrganizacaoCore _organizacaoCore;

        public NivelClassificacaoCore(IScdRepositories repositories, NivelClassificacaoValidation validation, IOrganizacaoCore organizacaoCore)
        {
            _unitOfWork = repositories.UnitOfWork;
            _niveisClassificacao = repositories.NiveisClassificacaoSpecific;
            _validation = validation;
            _organizacaoCore = organizacaoCore;
        }

        public async Task<int> CountAsync(Guid guidOrganizacao)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            var count = await _niveisClassificacao.CountByOrganizacaoAsync(guidOrganizacao);

            return count;
        }

        public async Task DeleteAsync(int id)
        {
            NivelClassificacaoModel nivelClassificacaoModel = await SearchAsync(id);

            _validation.CanDelete(nivelClassificacaoModel);

            await _niveisClassificacao.RemoveAsync(nivelClassificacaoModel.Id);
        }

        public async Task<NivelClassificacaoModel> InsertAsync(NivelClassificacaoModel nivelClassificacaoModel)
        {
            _validation.BasicValid(nivelClassificacaoModel);

            _validation.IdInsertValid(nivelClassificacaoModel.Id);

            nivelClassificacaoModel.Organizacao = await GetOrganizacao();

            nivelClassificacaoModel.Ativo = true;

            nivelClassificacaoModel = await _niveisClassificacao.AddAsync(nivelClassificacaoModel);

            return nivelClassificacaoModel;
        }

        public async Task<NivelClassificacaoModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            NivelClassificacaoModel nivelClassificacaoModel = await _niveisClassificacao.SearchAsync(id);

            _validation.Found(nivelClassificacaoModel);

            return nivelClassificacaoModel;
        }

        public async Task<ICollection<NivelClassificacaoModel>> SearchAsync(Guid guidOrganizacao, int page, int count)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            _validation.PaginationSearch(page, count);

            ICollection<NivelClassificacaoModel> niveisClassificacaoModel = await _niveisClassificacao.SearchByOrganizacaoAsync(guidOrganizacao, page, count);

            return niveisClassificacaoModel;
        }

        public async Task UpdateAsync(NivelClassificacaoModel nivelClassificacaoModel)
        {
            _validation.Valid(nivelClassificacaoModel);

            NivelClassificacaoModel nivelClassificacaoModelOld = await SearchAsync(nivelClassificacaoModel.Id);

            _validation.CanUpdate(nivelClassificacaoModel, nivelClassificacaoModelOld);

            nivelClassificacaoModel.Organizacao = await GetOrganizacao();

            await _niveisClassificacao.UpdateAsync(nivelClassificacaoModel);
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