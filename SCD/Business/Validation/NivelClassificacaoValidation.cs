using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System;

namespace Prodest.Scd.Business.Validation
{
    public class NivelClassificacaoValidation : CommonValidation
    {
        private IGenericRepository<NivelClassificacao>  _niveisClassificacao;

        public NivelClassificacaoValidation(IScdRepositories repositories)
        {
            _niveisClassificacao = repositories.NiveisClassificacao;
        }

        #region Valid
        internal void Valid(NivelClassificacaoModel nivelClassificacao)
        {
            BasicValid(nivelClassificacao);

            IdValid(nivelClassificacao.Id);
        }

        internal void IdInsertValid(int id)
        {
            if (id != default(int))
                throw new ScdException("O id não deve ser preenchido.");
        }

        #endregion

        #region Basic Valid
        internal void BasicValid(NivelClassificacaoModel nivelClassificacao)
        {
            NotNull(nivelClassificacao);

            Filled(nivelClassificacao);

            OrganizacaoNotNull(nivelClassificacao.Organizacao);
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

        internal void Found(NivelClassificacao nivelClassificacao)
        {
            if (nivelClassificacao == null)
                throw new ScdException("Nivel de Classificação não encontrado.");
        }

        internal void PaginationSearch(int page, int count)
        {
            if (page <= 0)
                throw new ScdException("Página inválida.");

            if (count <= 0)
                throw new ScdException("Quantidade de rgistro por página inválida.");
        }

        internal void CanDelete(NivelClassificacao nivelClassificacao)
        {
            if (nivelClassificacao.ItensPlanoClassificacao != null && nivelClassificacao.ItensPlanoClassificacao.Count > 0)
                throw new ScdException("O Nivel de Classificação possui itens e não pode ser excluído.");
        }

    }
}
