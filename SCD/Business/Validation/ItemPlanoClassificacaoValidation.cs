using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;

namespace Prodest.Scd.Business.Validation
{
    public class ItemPlanoClassificacaoValidation : CommonValidation
    {
        internal void Valid(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            IdValid(itemPlanoClassificacaoModel.Id);

            BasicValid(itemPlanoClassificacaoModel);
        }

        #region Basic Valid
        internal void BasicValid(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            NotNull(itemPlanoClassificacao);

            PlanoClassificacaoNotNull(itemPlanoClassificacao.PlanoClassificacao);

            NivelClassificacaoNotNull(itemPlanoClassificacao.NivelClassificacao);

            Filled(itemPlanoClassificacao);
        }

        private void NotNull(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            if (itemPlanoClassificacao == null)
                throw new ScdException("O Item Plano de Classificação não pode ser nulo.");
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
        #endregion

        internal void Found(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            if (itemPlanoClassificacao == null)
                throw new ScdException("Item do Plano de Classificação não encontrado.");
        }

        internal void CanDelete(ItemPlanoClassificacaoModel itemPlanoClassificacao)
        {
            if (itemPlanoClassificacao.ItensPlanoClassificacaoChildren != null && itemPlanoClassificacao.ItensPlanoClassificacaoChildren.Count > 0)
                throw new ScdException("O Itemd do Plano de Classificação possui itens e não pode ser excluído.");
        }
    }
}
