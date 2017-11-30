using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using System;

namespace Prodest.Scd.Business.Validation
{
    public class FundamentoLegalValidation : CommonValidation
    {
        #region Valid
        internal void Valid(FundamentoLegalModel fundamentoLegal)
        {
            BasicValid(fundamentoLegal);

            IdValid(fundamentoLegal.Id);
        }
        #endregion

        #region Basic Valid
        internal void BasicValid(FundamentoLegalModel fundamentoLegal)
        {
            NotNull(fundamentoLegal);

            Filled(fundamentoLegal);

            //OrganizacaoNotNull(fundamentoLegal.Organizacao);
        }

        private void NotNull(FundamentoLegalModel fundamentoLegal)
        {
            if (fundamentoLegal == null)
                throw new ScdException("O Nivel de Classificação não pode ser nulo.");
        }

        #region Filled
        internal void Filled(FundamentoLegalModel fundamentoLegal)
        {
            DescricaoFilled(fundamentoLegal.Descricao);
        }

        private void DescricaoFilled(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao) || string.IsNullOrWhiteSpace(descricao.Trim()))
                throw new ScdException("A descrição não pode ser vazia ou nula.");
        }

        private void OrganizacaoNotNull(OrganizacaoModel organizacao)
        {
            if (organizacao == null)
                throw new ScdException("A organização não pode ser nula.");
        }
        #endregion
        #endregion

        internal void Found(FundamentoLegalModel fundamentoLegalModel)
        {
            if (fundamentoLegalModel == null)
                throw new ScdException("Nivel de Classificação não encontrado.");
        }

        internal void CanDelete(FundamentoLegalModel fundamentoLegalModel)
        {
            //TODO: Implementar quando tiver Critério Associado.
            //if (fundamentoLegalModel.ItensPlanoClassificacao != null && fundamentoLegalModel.ItensPlanoClassificacao.Count > 0)
            //    throw new ScdException("O Nivel de Classificação possui itens e não pode ser excluído.");
        }

        internal void CanUpdate(FundamentoLegalModel newFundamentoLegalModel, FundamentoLegalModel oldFundamentoLegalModel)
        {
            if (newFundamentoLegalModel.Organizacao != null && (oldFundamentoLegalModel.Organizacao.Id != newFundamentoLegalModel.Organizacao.Id || !oldFundamentoLegalModel.Organizacao.GuidOrganizacao.Equals(newFundamentoLegalModel.Organizacao.GuidOrganizacao)))
            {
                throw new ScdException("Não é possível atualizar a Organização do Nível de Classificação.");
            }
        }
    }
}
