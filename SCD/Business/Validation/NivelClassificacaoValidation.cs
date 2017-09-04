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

        internal void IdValid(int id)
        {
            if (id == default(int))
                throw new ScdException("O id não pode ser nulo ou vazio.");
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

            OrganizacaoValid(nivelClassificacao.GuidOrganizacao);

            PublicacaoValid(nivelClassificacao.Publicacao, nivelClassificacao.Aprovacao, nivelClassificacao.InicioVigencia);

            FimVigenciaValid(nivelClassificacao.FimVigencia, nivelClassificacao.InicioVigencia);
        }

        private void NotNull(NivelClassificacaoModel nivelClassificacao)
        {
            if (nivelClassificacao == null)
                throw new ScdException("O Nivel de Classificação não pode ser nulo.");
        }

        #region Filled
        internal void Filled(NivelClassificacaoModel nivelClassificacao)
        {
            CodigoFilled(nivelClassificacao.Codigo);
            DescricaoFilled(nivelClassificacao.Descricao);
            OrganizacaoFilled(nivelClassificacao.GuidOrganizacao);
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

        internal void CanUpdate(NivelClassificacao nivelClassificacao)
        {
            if (nivelClassificacao.Publicacao.HasValue)
                throw new ScdException("O Nivel de Classificação possui data de publicação e não pode ser atualizado.");
        }

        internal void CanDelete(NivelClassificacao nivelClassificacao)
        {
            if (nivelClassificacao.Publicacao.HasValue)
                throw new ScdException("O Nivel de Classificação possui data de publicação e não pode ser excluído.");

            if (nivelClassificacao.ItensNivelClassificacao != null && nivelClassificacao.ItensNivelClassificacao.Count > 0)
                throw new ScdException("O Nivel de Classificação possui itens e não pode ser excluído.");
        }

    }
}
