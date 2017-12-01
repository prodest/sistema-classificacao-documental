using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class CriterioRestricaoCore : ICriterioRestricaoCore
    {
        private ICriterioRestricaoRepository _criteriosRestricao;

        private CriterioRestricaoValidation _validation;

        public CriterioRestricaoCore(IScdRepositories repositories, CriterioRestricaoValidation validation)
        {
            _criteriosRestricao = repositories.CriteriosRestricaoSpecific;

            _validation = validation;
        }

        public async Task<CriterioRestricaoModel> InsertAsync(CriterioRestricaoModel criterioRestricaoModel)
        {
            await _validation.BasicValid(criterioRestricaoModel);

            _validation.IdInsertValid(criterioRestricaoModel.Id);

            await _validation.CanInsert(criterioRestricaoModel);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            criterioRestricaoModel = await _criteriosRestricao.AddAsync(criterioRestricaoModel);

            return criterioRestricaoModel;
        }

        public async Task<CriterioRestricaoModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            CriterioRestricaoModel criterioRestricaoModel = await _criteriosRestricao.SearchAsync(id);

            _validation.Found(criterioRestricaoModel);

            return criterioRestricaoModel;
        }

        public async Task<ICollection<CriterioRestricaoModel>> SearchByPlanoClassificacaoAsync(int idPlanoClassificacao)
        {
            _validation.IdValid(idPlanoClassificacao);
            ICollection<CriterioRestricaoModel> criteriosRestricaoModel = await _criteriosRestricao.SearchByPlanoClassificacaoAsync(idPlanoClassificacao);
            return criteriosRestricaoModel;
        }
        

        public async Task UpdateAsync(CriterioRestricaoModel criterioRestricaoModel)
        {
            await _validation.Valid(criterioRestricaoModel);

            CriterioRestricaoModel criterioRestricaoModelOld = await _criteriosRestricao.SearchAsync(criterioRestricaoModel.Id);

            _validation.Found(criterioRestricaoModelOld);

            _validation.PlanoClassificacaoEquals(criterioRestricaoModel, criterioRestricaoModelOld);

            await _validation.CanUpdate(criterioRestricaoModelOld);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            await _criteriosRestricao.UpdateAsync(criterioRestricaoModel);
        }

        public async Task DeleteAsync(int id)
        {
            CriterioRestricaoModel criterioRestricaoModel = await _criteriosRestricao.SearchAsync(id);

            _validation.Found(criterioRestricaoModel);

            await _validation.CanDelete(criterioRestricaoModel);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            await _criteriosRestricao.RemoveAsync(criterioRestricaoModel.Id);
        }
    }
}