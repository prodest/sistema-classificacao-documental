using AutoMapper;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Integration.Organograma.Base;
using Prodest.Scd.Integration.Organograma.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class PlanoClassificacaoCore : IPlanoClassificacaoCore
    {
        private PlanoClassificacaoValidation _validation;

        private IPlanoClassificacaoRepository _planosClassificacao;
        private IOrganogramaService _organogramaService;
        private IOrganizacaoCore _organizacaoCore;

        public PlanoClassificacaoCore(IScdRepositories repositories, IOrganogramaService organogramaService, IOrganizacaoCore organizacaoCore)
        {
            _validation = new PlanoClassificacaoValidation();

            _planosClassificacao = repositories.PlanosClassificacaoSpecific;
            _organogramaService = organogramaService;
            _organizacaoCore = organizacaoCore;
        }

        public async Task<int> CountAsync(Guid guidOrganizacao)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            var count = await _planosClassificacao.CountByOrganizacaoAsync(guidOrganizacao);

            return count;
        }

        public async Task DeleteAsync(int id)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await SearchAsync(id);

            _validation.CanDelete(planoClassificacaoModel);

            await _planosClassificacao.RemoveAsync(planoClassificacaoModel.Id);
        }

        public async Task<PlanoClassificacaoModel> InsertAsync(PlanoClassificacaoModel planoClassificacaoModel)
        {
            _validation.BasicValid(planoClassificacaoModel);

            _validation.IdInsertValid(planoClassificacaoModel.Id);

            planoClassificacaoModel.GuidOrganizacao = GetGuidOrganizacao();
            planoClassificacaoModel.Organizacao = await GetOrganizacaoPatricarcaModel();

            planoClassificacaoModel = await _planosClassificacao.AddAsync(planoClassificacaoModel);

            return planoClassificacaoModel;
        }

        public async Task<PlanoClassificacaoModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            PlanoClassificacaoModel planoClassificacaoModel = await _planosClassificacao.SearchAsync(id);

            _validation.Found(planoClassificacaoModel);

            return planoClassificacaoModel;
        }

        public async Task<ICollection<PlanoClassificacaoModel>> SearchAsync(Guid guidOrganizacao, int page, int count)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            _validation.PaginationSearch(page, count);

            ICollection<PlanoClassificacaoModel> planosClassificacaoModel = await _planosClassificacao.SearchByOrganizacaoAsync(guidOrganizacao, page, count);

            return planosClassificacaoModel;
        }

        public async Task UpdateAsync(PlanoClassificacaoModel planoClassificacaoModel)
        {
            _validation.Valid(planoClassificacaoModel);

            PlanoClassificacaoModel planoClassificacaoModelOld = await SearchAsync(planoClassificacaoModel.Id);

            _validation.CanUpdate(planoClassificacaoModelOld);

            planoClassificacaoModel.GuidOrganizacao = GetGuidOrganizacao();
            planoClassificacaoModel.Organizacao = await GetOrganizacaoPatricarcaModel();

            await _planosClassificacao.UpdateAsync(planoClassificacaoModel);
        }

        public async Task UpdateFimVigenciaAsync(int id, DateTime fimVigencia)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await SearchAsync(id);

            _validation.FimVigenciaValid(fimVigencia, planoClassificacaoModel.InicioVigencia);

            planoClassificacaoModel.FimVigencia = fimVigencia;

            await _planosClassificacao.UpdateAsync(planoClassificacaoModel);
        }

        private Guid GetGuidOrganizacao()
        {
            //TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            Guid guidProdest = new Guid(Environment.GetEnvironmentVariable("guidProdest"));

            return guidProdest;
        }

        private async Task<OrganizacaoModel> GetOrganizacaoPatricarcaModel()
        {
            //TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            OrganogramaOrganizacao organogramaOrganizacaoPatriarca = await _organogramaService.SearchPatriarcaAsync(GetGuidOrganizacao());

            OrganizacaoModel organizacaoPatriarca = await _organizacaoCore.SearchAsync(organogramaOrganizacaoPatriarca.Guid);

            return organizacaoPatriarca;
        }
    }
}