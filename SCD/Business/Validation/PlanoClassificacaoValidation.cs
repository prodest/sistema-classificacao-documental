using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using Prodest.Scd.Infrastructure.Common.Exceptions;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System;
using System.Collections.Generic;

namespace Prodest.Scd.Business.Validation
{
    public class PlanoClassificacaoValidation : CommonValidation
    {
        private IGenericRepository<PlanoClassificacao>  _planosClassificacao;

        public PlanoClassificacaoValidation(IScdRepositories repositories)
        {
            _planosClassificacao = repositories.PlanosClassificacao;
        }

        #region Valid
        internal void Valid(PlanoClassificacaoModel planoClassificacao)
        {
            BasicValid(planoClassificacao);

            IdValid(planoClassificacao.Id);
        }

        internal void IdValid(int id)
        {
            if (id == default(int))
                throw new ScdExpection("O id deve não pode ser nulo ou vazio.");
        }
        #endregion

        #region Basic Valid
        internal void BasicValid(PlanoClassificacaoModel planoClassificacao)
        {
            NotNull(planoClassificacao);

            Filled(planoClassificacao);

            OrganizacaoValid(planoClassificacao.GuidOrganizacao);

            PublicacaoValid(planoClassificacao.Publicacao, planoClassificacao.Aprovacao, planoClassificacao.InicioVigencia);

            FimVigenciaValid(planoClassificacao.FimVigencia, planoClassificacao.InicioVigencia);
        }

        private void NotNull(PlanoClassificacaoModel planoClassificacao)
        {
            if (planoClassificacao == null)
                throw new ScdExpection("O Plano de Classificação não pode ser nulo.");
        }

        #region Filled
        internal void Filled(PlanoClassificacaoModel planoClassificacao)
        {
            CodigoFilled(planoClassificacao.Codigo);
            DescricaoFilled(planoClassificacao.Descricao);
            OrganizacaoFilled(planoClassificacao.GuidOrganizacao);
        }

        private void CodigoFilled(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(codigo.Trim()))
                throw new ScdExpection("O código não pode ser vazio ou nulo.");
        }

        private void DescricaoFilled(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao) || string.IsNullOrWhiteSpace(descricao.Trim()))
                throw new ScdExpection("A descrição não pode ser vazia ou nula.");
        }

        #endregion

        private void PublicacaoValid(DateTime? publicacao, DateTime? aprovacao, DateTime? inicioVigencia)
        {
            if (publicacao.HasValue && !aprovacao.HasValue)
                throw new ScdExpection("A data de aprovação não pode ser vazia ou nula quando existe uma data de publicação.");

            if (publicacao.HasValue && !inicioVigencia.HasValue)
                throw new ScdExpection("A data de início de vigência não pode ser vazia ou nula quando existe uma data de publicação.");

            if ((publicacao.HasValue && aprovacao.HasValue) && (publicacao.Value < aprovacao.Value))
                throw new ScdExpection("A data de publicação deve ser maior ou igual à data de aprovação.");
        }

        internal void FimVigenciaValid(DateTime? fimVigencia, DateTime? inicioVigencia)
        {
            if (fimVigencia.HasValue && !inicioVigencia.HasValue)
                throw new ScdExpection("A data de início de vigência não pode ser vazia ou nula quando existe uma data de fim de vigência.");

            if ((fimVigencia.HasValue && inicioVigencia.HasValue) && (fimVigencia.Value < inicioVigencia.Value))
                throw new ScdExpection("A data de fim de vigência deve ser maior ou igual à data de início de vigência.");
        }
        #endregion

        internal void Found(PlanoClassificacao planoClassificacao)
        {
            if (planoClassificacao == null)
                throw new ScdExpection("Plano de Classificação não encontrado.");
        }

        internal void PaginationSearch(int page, int count)
        {
            if (page <= 0)
                throw new ScdExpection("Página inválida.");

            if (count <= 0)
                throw new ScdExpection("Quantidade de rgistro por página inválida.");
        }

        internal void CanUpDate(PlanoClassificacao planoClassificacaoModel)
        {
            if (planoClassificacaoModel.Publicacao.HasValue)
                throw new ScdExpection("O Plano de Classificação possui data de publicação e não pode ser atualizado.");
        }
    }
}
