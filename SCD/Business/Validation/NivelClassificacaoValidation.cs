using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using System;

namespace Prodest.Scd.Business.Validation
{
    public class NivelClassificacaoValidation : CommonValidation
    {
        #region Valid
        internal void Valid(NivelClassificacaoModel nivelClassificacao)
        {
            BasicValid(nivelClassificacao);

            IdValid(nivelClassificacao.Id);
        }
        #endregion

        #region Basic Valid
        internal void BasicValid(NivelClassificacaoModel nivelClassificacao)
        {
            NotNull(nivelClassificacao);

            Filled(nivelClassificacao);

            //OrganizacaoNotNull(nivelClassificacao.Organizacao);
        }

        private void NotNull(NivelClassificacaoModel nivelClassificacao)
        {
            if (nivelClassificacao == null)
                throw new ScdException("O Nivel de Classificação não pode ser nulo.");
        }

        #region Filled
        internal void Filled(NivelClassificacaoModel nivelClassificacao)
        {
            DescricaoFilled(nivelClassificacao.Descricao);
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

        internal void Found(NivelClassificacaoModel nivelClassificacaoModel)
        {
            if (nivelClassificacaoModel == null)
                throw new ScdException("Nivel de Classificação não encontrado.");
        }

        internal void CanDelete(NivelClassificacaoModel nivelClassificacaoModel)
        {
            if (nivelClassificacaoModel.ItensPlanoClassificacao != null && nivelClassificacaoModel.ItensPlanoClassificacao.Count > 0)
                throw new ScdException("O Nivel de Classificação possui itens e não pode ser excluído.");
        }

        internal void CanUpdate(NivelClassificacaoModel newNivelClassificacaoModel, NivelClassificacaoModel oldNivelClassificacaoModel)
        {
            if (newNivelClassificacaoModel.Organizacao != null && (oldNivelClassificacaoModel.Organizacao.Id != newNivelClassificacaoModel.Organizacao.Id || !oldNivelClassificacaoModel.Organizacao.GuidOrganizacao.Equals(newNivelClassificacaoModel.Organizacao.GuidOrganizacao)))
            {
                throw new ScdException("Não é possível atualizar a Organização do Nível de Classificação.");
            }
        }
    }
}
