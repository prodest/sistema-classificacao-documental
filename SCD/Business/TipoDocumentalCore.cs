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
    public class TipoDocumentalCore : ITipoDocumentalCore
    {
        private TipoDocumentalValidation _validation;
        private IUnitOfWork _unitOfWork;
        private ITipoDocumentalRepository _tiposDocumentais;
        private IOrganizacaoCore _organizacaoCore;

        public TipoDocumentalCore(TipoDocumentalValidation validation, IScdRepositories repositories, IOrganizacaoCore organizacaoCore)
        {
            _validation = validation;
            _unitOfWork = repositories.UnitOfWork;
            _tiposDocumentais = repositories.TiposDocumentaisSpecific;
            _organizacaoCore = organizacaoCore;
        }

        public async Task<int> CountAsync(Guid guidOrganizacao)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            var count = await _tiposDocumentais.CountByOrganizacaoAsync(guidOrganizacao);

            return count;
        }

        public async Task DeleteAsync(int id)
        {
            TipoDocumentalModel tipoDocumentalModel = await SearchAsync(id);

            _validation.CanDelete(tipoDocumentalModel);

            await _tiposDocumentais.RemoveAsync(tipoDocumentalModel.Id);
        }

        public async Task<TipoDocumentalModel> InsertAsync(TipoDocumentalModel tipoDocumentalModel)
        {
            _validation.BasicValid(tipoDocumentalModel);

            _validation.IdInsertValid(tipoDocumentalModel.Id);

            tipoDocumentalModel.Organizacao = await GetOrganizacao();
            tipoDocumentalModel.Ativo = true;

            tipoDocumentalModel = await _tiposDocumentais.AddAsync(tipoDocumentalModel);

            return tipoDocumentalModel;
        }

        public async Task<TipoDocumentalModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            TipoDocumentalModel tipoDocumentalModel = await _tiposDocumentais.SearchAsync(id);

            _validation.Found(tipoDocumentalModel);

            return tipoDocumentalModel;
        }

        public async Task<ICollection<TipoDocumentalModel>> SearchAsync(Guid guidOrganizacao, int page, int count)
        {
            _validation.OrganizacaoValid(guidOrganizacao);

            _validation.PaginationSearch(page, count);

            ICollection<TipoDocumentalModel> tiposDocumentaisModel = await _tiposDocumentais.SearchByOrganizacaoAsync(guidOrganizacao, page, count);

            return tiposDocumentaisModel;
        }

        public async Task UpdateAsync(TipoDocumentalModel tipoDocumentalModel)
        {
            _validation.Valid(tipoDocumentalModel);

            TipoDocumentalModel tipoDocumentalModelOld = await SearchAsync(tipoDocumentalModel.Id);

            _validation.CanUpdate(tipoDocumentalModel, tipoDocumentalModelOld);

            tipoDocumentalModel.Organizacao = await GetOrganizacao();

            await _tiposDocumentais.UpdateAsync(tipoDocumentalModel);
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