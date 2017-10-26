using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Validation
{
    public class ItemPlanoClassificacaoValidation : CommonValidation
    {
        private INivelClassificacaoCore _nivelClassificacaoCore;
        private IPlanoClassificacaoCore _planoClassificacaoCore;

        private PlanoClassificacaoValidation _planoClassificacaoValidation;

        public ItemPlanoClassificacaoValidation(INivelClassificacaoCore nivelClassificacaoCore, IPlanoClassificacaoCore planoClassificacaoCore, PlanoClassificacaoValidation planoClassificacaoValidation)
        {
            _nivelClassificacaoCore = nivelClassificacaoCore;
            _planoClassificacaoCore = planoClassificacaoCore;
            _planoClassificacaoValidation = planoClassificacaoValidation;
        }

        internal async Task Valid(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            await BasicValid(itemPlanoClassificacaoModel);

            IdValid(itemPlanoClassificacaoModel.Id);
        }

        #region Basic Valid
        internal async Task BasicValid(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            NotNull(itemPlanoClassificacao);

            NivelClassificacaoNotNull(itemPlanoClassificacao.NivelClassificacao);

            PlanoClassificacaoNotNull(itemPlanoClassificacao.PlanoClassificacao);

            Filled(itemPlanoClassificacao);

            await NivelClassificacaoExists(itemPlanoClassificacao.NivelClassificacao);

            await PlanoClassificacaoExists(itemPlanoClassificacao.PlanoClassificacao);
        }

        private void NotNull(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            if (itemPlanoClassificacao == null)
                throw new ScdException("O Item do Plano de Classificação não pode ser nulo.");
        }

        private void PlanoClassificacaoNotNull(PlanoClassificacaoModel planoClassificacao)
        {
            if (planoClassificacao == null)
                throw new ScdException("O Plano de Classificação não pode ser nulo.");
        }

        private void NivelClassificacaoNotNull(NivelClassificacaoModel nivelClassificacao)
        {
            if (nivelClassificacao == null)
                throw new ScdException("O Nível de Classificação não pode ser nulo.");
        }

        #region Filled
        internal void Filled(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            CodigoFilled(itemPlanoClassificacao.Codigo);
            DescricaoFilled(itemPlanoClassificacao.Descricao);
            PlanoClassificacaoFilled(itemPlanoClassificacao.PlanoClassificacao);
            NivelClassificacaoFilled(itemPlanoClassificacao.NivelClassificacao);
        }

        private void CodigoFilled(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(codigo.Trim()))
                throw new ScdException("O código não pode ser vazio ou nulo.");
        }

        private void DescricaoFilled(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao) || string.IsNullOrWhiteSpace(descricao.Trim()))
                throw new ScdException("A descrição não pode ser vazia ou nula.");
        }

        private void PlanoClassificacaoFilled(PlanoClassificacaoModel planoClassificacao)
        {
            if (planoClassificacao.Id == default(int))
                throw new ScdException("Identificador do Plano de Classificação inválido.");
        }

        private void NivelClassificacaoFilled(NivelClassificacaoModel nivelClassificacao)
        {
            if (nivelClassificacao.Id == default(int))
                throw new ScdException("Identificador do Nível de Classificação inválido.");
        }
        #endregion

        internal async Task NivelClassificacaoExists(NivelClassificacaoModel nivelClassificacaoModel)
        {
            nivelClassificacaoModel = await _nivelClassificacaoCore.SearchAsync(nivelClassificacaoModel.Id);

            if (nivelClassificacaoModel == null)
                throw new ScdException("Nível de Classificação não encontrado.");
        }

        internal async Task PlanoClassificacaoExists(PlanoClassificacaoModel planoClassificacaoModel)
        {
            planoClassificacaoModel = await _planoClassificacaoCore.SearchAsync(planoClassificacaoModel.Id);

            if (planoClassificacaoModel == null)
                throw new ScdException("Plano de Classificação não encontrado.");
        }
        #endregion

        internal void Found(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            if (itemPlanoClassificacao == null)
                throw new ScdException("Item do Plano de Classificação não encontrado.");
        }

        internal void PlanoClassificacaoEquals(ItemPlanoClassificacaoModel itemPlanoClassificacaoModelNew, ItemPlanoClassificacaoModel itemPlanoClassificacaoModelOld)
        {
            if (itemPlanoClassificacaoModelNew.PlanoClassificacao.Id != itemPlanoClassificacaoModelOld.PlanoClassificacao.Id)
                throw new ScdException("O Plano de Classificação não pode ser alterado.");
        }

        internal async Task CanUpdate(ItemPlanoClassificacaoModel itemPlanoClassificacaoModelOld)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await _planoClassificacaoCore.SearchAsync(itemPlanoClassificacaoModelOld.PlanoClassificacao.Id);
            _planoClassificacaoValidation.CanUpdate(planoClassificacaoModel);
        }

        internal async Task CanDelete(ItemPlanoClassificacaoModel itemPlanoClassificacao, int countChildren)
        {
            await CanUpdate(itemPlanoClassificacao);

            if (countChildren > 0)
                throw new ScdException("O Item do Plano de Classificação possui itens e não pode ser excluído.");
        }
    }
}