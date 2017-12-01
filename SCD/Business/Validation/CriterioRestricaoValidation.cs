using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation.Common;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Validation
{
    public class CriterioRestricaoValidation : CommonValidation
    {
        private ICriterioRestricaoRepository _criteriosRestricao;
        private IFundamentoLegalRepository _fundamentosLegais;
        private IPlanoClassificacaoRepository _planosClassificacao;

        private PlanoClassificacaoValidation _planoClassificacaoValidation;

        public CriterioRestricaoValidation(IScdRepositories repositories, PlanoClassificacaoValidation planoClassificacaoValidation)
        {
            _fundamentosLegais = repositories.FundamentosLegaisSpecific;
            _planosClassificacao = repositories.PlanosClassificacaoSpecific;

            _planoClassificacaoValidation = planoClassificacaoValidation;
        }

        internal async Task Valid(CriterioRestricaoModel criterioRestricaoModel)
        {
            await BasicValid(criterioRestricaoModel);

            IdValid(criterioRestricaoModel.Id);
        }

        #region Basic Valid
        internal async Task BasicValid(CriterioRestricaoModel criterioRestricaoModel)
        {
            NotNull(criterioRestricaoModel);

            PlanoClassificacaoNotNull(criterioRestricaoModel.PlanoClassificacao);
            FundamentoLegalNotNull(criterioRestricaoModel.FundamentoLegal);

            Filled(criterioRestricaoModel);

            await PlanoClassificacaoExists(criterioRestricaoModel.PlanoClassificacao);
            await FundamentoLegalExists(criterioRestricaoModel.FundamentoLegal);

            await PlanoClassificacaoFundamenoLegalBelongSameOrganizacao(criterioRestricaoModel.PlanoClassificacao, criterioRestricaoModel.FundamentoLegal);

            PrazoTerminoNotExceedLimit(criterioRestricaoModel);
        }

        private void NotNull(CriterioRestricaoModel criterioRestricaoModel)
        {
            if (criterioRestricaoModel == null)
                throw new ScdException("O CriterioRestricao não pode ser nulo.");
        }

        private void PlanoClassificacaoNotNull(PlanoClassificacaoModel planoClassificacaoModel)
        {
            if (planoClassificacaoModel == null)
                throw new ScdException("O Plano de Classificação não pode ser nulo.");
        }

        private void FundamentoLegalNotNull(FundamentoLegalModel fundamentoLegal)
        {
            if (fundamentoLegal == null)
                throw new ScdException("O Fundamento Legal não pode ser nulo.");
        }

        #region Filled
        internal void Filled(CriterioRestricaoModel criterioRestricaoModel)
        {
            PlanoClassificacaoFilled(criterioRestricaoModel.PlanoClassificacao);
            CodigoFilled(criterioRestricaoModel.Codigo);
            DescricaoFilled(criterioRestricaoModel.Descricao);
            FundamentoLegalFilled(criterioRestricaoModel.FundamentoLegal);
            JustificativaFilled(criterioRestricaoModel.Justificativa);
            PrazoTerminoOrEventoFimFilled(criterioRestricaoModel);
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

        private void PrazoTerminoOrEventoFimFilled(CriterioRestricaoModel criterioRestricaoModel)
        {
            if (criterioRestricaoModel.Classificavel)
            {
                if (!criterioRestricaoModel.PrazoTermino.HasValue)
                    throw new ScdException("O Prazo de Términio deve ser preenchido quando o Critério de Restrição é classificável.");

                if (!criterioRestricaoModel.UnidadePrazoTermino.HasValue)
                    throw new ScdException("A Unidade do Prazo de Términio deve ser preenchida quando o Critério de Restrição é classificável.");
            }
            else
            {
                if (criterioRestricaoModel.PrazoTermino.HasValue)
                    throw new ScdException("O Prazo de Términio não deve ser preenchido quando o Critério de Restrição não é classificável.");

                if (criterioRestricaoModel.UnidadePrazoTermino.HasValue)
                    throw new ScdException("A Unidade do Prazo de Términio não deve ser preenchida quando o Critério de Restrição não é classificável.");

                if (criterioRestricaoModel.EventoFim != null)
                    throw new ScdException("Evento Fim não deve ser preenchido quando o Critério de Restrição não é classificável.");
            }
        }

        private void JustificativaFilled(string justificativa)
        {
            if (string.IsNullOrWhiteSpace(justificativa))
                throw new ScdException("A Justificativa não pode ser vazia ou nula.");
        }

        private void FundamentoLegalFilled(FundamentoLegalModel fundamentoLegalModel)
        {
            if (fundamentoLegalModel.Id == default(int))
                throw new ScdException("Identificador do Fundamento Legal inválido.");
        }

        private void PlanoClassificacaoFilled(PlanoClassificacaoModel planoClassificacaoModel)
        {
            if (planoClassificacaoModel.Id == default(int))
                throw new ScdException("Identificador do Plano Classificação inválido.");
        }
        #endregion

        internal async Task PlanoClassificacaoExists(PlanoClassificacaoModel planoClassificacaoModel)
        {
            planoClassificacaoModel = await _planosClassificacao.SearchAsync(planoClassificacaoModel.Id);

            if (planoClassificacaoModel == null)
                throw new ScdException("Plano de Classificação não encontrado.");
        }

        private async Task FundamentoLegalExists(FundamentoLegalModel fundamentoLegal)
        {
            fundamentoLegal = await _fundamentosLegais.SearchAsync(fundamentoLegal.Id);
            
            if (fundamentoLegal == null)
                throw new ScdException("Fundamento Legal não encontrado.");
        }

        private async Task PlanoClassificacaoFundamenoLegalBelongSameOrganizacao(PlanoClassificacaoModel planoClassificacao, FundamentoLegalModel fundamentoLegal)
        {
            planoClassificacao = await _planosClassificacao.SearchAsync(planoClassificacao.Id);
            fundamentoLegal = await _fundamentosLegais.SearchAsync(fundamentoLegal.Id);

            if (planoClassificacao.OrganizacaoPatriarca.Id != fundamentoLegal.OrganizacaoPatriarca.Id)
                throw new ScdException("A Organização Patriarca do Plano de Classificação deve ser igual à do Fundamento Legal.");
        }

        private void PrazoTerminoNotExceedLimit(CriterioRestricaoModel criterioRestricaoModel)
        {
            if (criterioRestricaoModel.Classificavel)
            {
                DateTime now = DateTime.Now;
                DateTime limitDate = now.AddYears(25);
                DateTime referenceDate = default(DateTime);

                if (UnidadeTempoModel.Dias.Equals(criterioRestricaoModel.UnidadePrazoTermino))
                    referenceDate = now.AddDays(criterioRestricaoModel.PrazoTermino.Value);
                else if (UnidadeTempoModel.Meses.Equals(criterioRestricaoModel.UnidadePrazoTermino))
                    referenceDate = now.AddMonths(criterioRestricaoModel.PrazoTermino.Value);
                else if (UnidadeTempoModel.Anos.Equals(criterioRestricaoModel.UnidadePrazoTermino))
                    referenceDate = now.AddYears(criterioRestricaoModel.PrazoTermino.Value);

                if (referenceDate > limitDate)
                    throw new ScdException("O Prazo de Términio não pode exceder 25 anos.");
            }
        }
        #endregion

        internal void Found(CriterioRestricaoModel criterioRestricaoModel)
        {
            if (criterioRestricaoModel == null)
                throw new ScdException("Critério de Restrição não encontrado.");
        }

        internal void PlanoClassificacaoEquals(CriterioRestricaoModel criterioRestricaoModelNew, CriterioRestricaoModel criterioRestricaoModelOld)
        {
            if (criterioRestricaoModelNew.PlanoClassificacao.Id != criterioRestricaoModelOld.PlanoClassificacao.Id)
                throw new ScdException("O Plano de Classificação não pode ser alterado.");
        }

        internal async Task CanInsert(CriterioRestricaoModel criterioRestricaoModel)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await _planosClassificacao.SearchAsync(criterioRestricaoModel.PlanoClassificacao.Id);

            DateTime now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day);

            if (planoClassificacaoModel.FimVigencia.HasValue && planoClassificacaoModel.FimVigencia.Value < now)
                throw new Exception("Não é possível inserir um Critério de Restrição em um Plano de Classificação com vigência expirada.");

            FundamentoLegalModel fundamentoLegalModel = await _fundamentosLegais.SearchAsync(criterioRestricaoModel.FundamentoLegal.Id);

            if (!fundamentoLegalModel.Ativo)
                throw new ScdException("Não é possível associar um Critério de Restrição a um Fundamento Legal inativo.");
        }

        internal async Task CanUpdate(CriterioRestricaoModel criterioRestricaoModel)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await _planosClassificacao.SearchAsync(criterioRestricaoModel.PlanoClassificacao.Id);

            DateTime now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day);

            if (planoClassificacaoModel.FimVigencia.HasValue && planoClassificacaoModel.FimVigencia.Value < now)
                throw new Exception("Não é possível alterar um Critério de Restrição de um Plano de Classificação com vigência expirada.");

            FundamentoLegalModel fundamentoLegalModel = await _fundamentosLegais.SearchAsync(criterioRestricaoModel.FundamentoLegal.Id);

            if (!fundamentoLegalModel.Ativo)
                throw new ScdException("Não é possível associar um Critério de Restrição a um Fundamento Legal inativo.");
        }

        internal async Task CanDelete(CriterioRestricaoModel criterioRestricaoModel)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await _planosClassificacao.SearchAsync(criterioRestricaoModel.PlanoClassificacao.Id);

            DateTime now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day);

            if (planoClassificacaoModel.FimVigencia.HasValue && planoClassificacaoModel.FimVigencia.Value < now)
                throw new Exception("Não é possível excluir um Critério de Restrição de um Plano de Classificação com vigência expirada.");

            CriterioRestricaoModel criterioRestricaoOldModel = await _criteriosRestricao.SearchAsync(criterioRestricaoModel.Id);

            if (criterioRestricaoOldModel.TermosClassificacaoInformacao != null && criterioRestricaoOldModel.TermosClassificacaoInformacao.Count > 0)
                throw new Exception("Não é possível excluir um Critério de Restrição associado a um Termo de Classificação daInformação.");

            if (criterioRestricaoOldModel.Documentos != null &&  criterioRestricaoOldModel.Documentos.Count > 0)
                throw new Exception("Não é possível excluir um Critério de Restrição associado a um Tipo Documental.");
        }
    }
}