using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class TemporalidadeCore : ITemporalidadeCore
    {
        private ITemporalidadeRepository _temporalidades;

        private TemporalidadeValidation _validation;

        public TemporalidadeCore(IScdRepositories repositories, TemporalidadeValidation validation)
        {
            _temporalidades = repositories.TemporalidadesSpecific;

            _validation = validation;
        }

        public async Task<TemporalidadeModel> InsertAsync(TemporalidadeModel temporalidadeModel)
        {
            await _validation.BasicValid(temporalidadeModel);

            _validation.IdInsertValid(temporalidadeModel.Id);

            await _validation.CanInsert(temporalidadeModel);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            temporalidadeModel = await _temporalidades.AddAsync(temporalidadeModel);

            return temporalidadeModel;
        }

        public async Task<TemporalidadeModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            TemporalidadeModel temporalidadeModel = await _temporalidades.SearchAsync(id);

            _validation.Found(temporalidadeModel);

            return temporalidadeModel;
        }

        public async Task UpdateAsync(TemporalidadeModel temporalidadeModel)
        {
            await _validation.Valid(temporalidadeModel);

            TemporalidadeModel temporalidadeModelOld = await _temporalidades.SearchAsync(temporalidadeModel.Id);

            _validation.Found(temporalidadeModelOld);

            await _validation.PlanoClassificacaoEquals(temporalidadeModel, temporalidadeModelOld);

            await _validation.CanUpdate(temporalidadeModelOld);
            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            await _temporalidades.UpdateAsync(temporalidadeModel);
        }

        public async Task DeleteAsync(int id)
        {
            TemporalidadeModel temporalidadeModel = await _temporalidades.SearchAsync(id);

            _validation.Found(temporalidadeModel);

            await _validation.CanDelete(temporalidadeModel);
            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            await _temporalidades.RemoveAsync(temporalidadeModel.Id);
        }
    }
}