using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation.Common;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Validation
{
    public class PlanoClassificacaoValidation : CommonValidation
    {
        private IItemPlanoClassificacaoRepository _itensPlanoClassificacao;

        public PlanoClassificacaoValidation(IScdRepositories repositories)
        {
            _itensPlanoClassificacao = repositories.ItensPlanoClassificacaoSpecific;
        }

        #region Valid
        internal void Valid(PlanoClassificacaoModel planoClassificacao)
        {
            BasicValid(planoClassificacao);

            IdValid(planoClassificacao.Id);
        }
        #endregion

        #region Basic Valid
        internal void BasicValid(PlanoClassificacaoModel planoClassificacao)
        {
            NotNull(planoClassificacao);

            Filled(planoClassificacao);

            OrganizacaoNull(planoClassificacao);

            PublicacaoValid(planoClassificacao.Publicacao, planoClassificacao.Aprovacao, planoClassificacao.InicioVigencia);

            FimVigenciaValid(planoClassificacao.FimVigencia, planoClassificacao.InicioVigencia);
        }

        private void NotNull(PlanoClassificacaoModel planoClassificacao)
        {
            if (planoClassificacao == null)
                throw new ScdException("O Plano de Classificação não pode ser nulo.");
        }

        #region Filled
        internal void Filled(PlanoClassificacaoModel planoClassificacao)
        {
            CodigoFilled(planoClassificacao.Codigo);
            DescricaoFilled(planoClassificacao.Descricao);
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

        private void OrganizacaoNull(PlanoClassificacaoModel planoClassificacao)
        {
            if (!planoClassificacao.GuidOrganizacao.Equals(Guid.Empty))
                throw new ScdException("Não é possível editar a Organização do Plano de Classificação.");
            if (planoClassificacao.Organizacao != null)
                throw new ScdException("Não é possível editar a Organização do Plano de Classificação.");
        }

        #endregion

        private void PublicacaoValid(DateTime? publicacao, DateTime? aprovacao, DateTime? inicioVigencia)
        {
            if (publicacao.HasValue && !aprovacao.HasValue)
                throw new ScdException("A data de aprovação não pode ser vazia ou nula quando existe uma data de publicação.");

            if (publicacao.HasValue && !inicioVigencia.HasValue)
                throw new ScdException("A data de início de vigência não pode ser vazia ou nula quando existe uma data de publicação.");

            if ((publicacao.HasValue && aprovacao.HasValue) && (publicacao.Value < aprovacao.Value))
                throw new ScdException("A data de publicação deve ser maior ou igual à data de aprovação.");
        }

        internal void FimVigenciaValid(DateTime? fimVigencia, DateTime? inicioVigencia)
        {
            if (fimVigencia.HasValue && !inicioVigencia.HasValue)
                throw new ScdException("A data de início de vigência não pode ser vazia ou nula quando existe uma data de fim de vigência.");

            if ((fimVigencia.HasValue && inicioVigencia.HasValue) && (fimVigencia.Value < inicioVigencia.Value))
                throw new ScdException("A data de fim de vigência deve ser maior ou igual à data de início de vigência.");
        }
        #endregion

        internal void Found(PlanoClassificacaoModel planoClassificacaoModel)
        {
            if (planoClassificacaoModel == null)
                throw new ScdException("Plano de Classificação não encontrado.");
        }

        internal void CanUpdate(PlanoClassificacaoModel planoClassificacaoModelOld)
        {
            if (planoClassificacaoModelOld.Publicacao.HasValue)
                throw new ScdException("O Plano de Classificação possui data de publicação e não pode ser atualizado.");
        }

        internal async Task CanDelete(PlanoClassificacaoModel planoClassificacaoModel)
        {
            if (planoClassificacaoModel.Publicacao.HasValue)
                throw new ScdException("O Plano de Classificação possui data de publicação e não pode ser excluído.");

            int countItensPlanoClassificacao = await _itensPlanoClassificacao.CountByPlanoClassificacao(planoClassificacaoModel.Id);

            if (countItensPlanoClassificacao > 0)
                throw new ScdException("O Plano de Classificação possui itens e não pode ser excluído.");
        }

    }
}