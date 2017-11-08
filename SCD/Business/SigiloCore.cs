using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class SigiloCore : ISigiloCore
    {
        private ISigiloRepository _sigilos;

        private SigiloValidation _validation;

        public SigiloCore(IScdRepositories repositories, SigiloValidation validation)
        {
            _sigilos = repositories.SigilosSpecific;

            _validation = validation;
        }

        public async Task<SigiloModel> InsertAsync(SigiloModel sigiloModel)
        {
            await _validation.BasicValid(sigiloModel);

            _validation.IdInsertValid(sigiloModel.Id);

            await _validation.CanInsert(sigiloModel);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            sigiloModel = await _sigilos.AddAsync(sigiloModel);

            return sigiloModel;
        }

        public async Task<SigiloModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            SigiloModel sigiloModel = await _sigilos.SearchAsync(id);

            _validation.Found(sigiloModel);

            return sigiloModel;
        }

        public async Task UpdateAsync(SigiloModel sigiloModel)
        {
            await _validation.Valid(sigiloModel);

            SigiloModel sigiloModelOld = await _sigilos.SearchAsync(sigiloModel.Id);

            _validation.Found(sigiloModelOld);

            await _validation.PlanoClassificacaoEquals(sigiloModel, sigiloModelOld);

            await _validation.CanUpdate(sigiloModelOld);
            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            await _sigilos.UpdateAsync(sigiloModel);
        }

        public async Task DeleteAsync(int id)
        {
            SigiloModel sigiloModel = await _sigilos.SearchAsync(id);

            _validation.Found(sigiloModel);

            await _validation.CanDelete(sigiloModel);
            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário

            await _sigilos.RemoveAsync(sigiloModel.Id);
        }
    }
}