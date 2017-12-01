using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation.Common;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Validation
{
    public class TemporalidadeValidation : CommonValidation
    {
        private IDocumentoRepository _documentos;
        private IPlanoClassificacaoRepository _planosClassificacao;

        private PlanoClassificacaoValidation _planoClassificacaoValidation;

        public TemporalidadeValidation(IScdRepositories repositories, PlanoClassificacaoValidation planoClassificacaoValidation)
        {
            _documentos = repositories.DocumentosSpecific;
            _planosClassificacao = repositories.PlanosClassificacaoSpecific;

            _planoClassificacaoValidation = planoClassificacaoValidation;
        }

        internal async Task Valid(TemporalidadeModel temporalidadeModel)
        {
            await BasicValid(temporalidadeModel);

            IdValid(temporalidadeModel.Id);
        }

        #region Basic Valid
        internal async Task BasicValid(TemporalidadeModel temporalidadeModel)
        {
            NotNull(temporalidadeModel);

            DocumentoNotNull(temporalidadeModel.Documento);

            Filled(temporalidadeModel);

            await DocumentoExists(temporalidadeModel.Documento);
        }

        private void NotNull(TemporalidadeModel temporalidadeModel)
        {
            if (temporalidadeModel == null)
                throw new ScdException("O Temporalidade não pode ser nulo.");
        }

        private void DocumentoNotNull(DocumentoModel documentoModel)
        {
            if (documentoModel == null)
                throw new ScdException("O Documento não pode ser nulo.");
        }

        private void TipoDocumentalNotNull(TipoDocumentalModel tipoDocumentalModel)
        {
            if (tipoDocumentalModel == null)
                throw new ScdException("O Tipo Documental não pode ser nulo.");
        }

        #region Filled
        internal void Filled(TemporalidadeModel temporalidadeModel)
        {
            CodigoFilled(temporalidadeModel.Codigo);
            DescricaoFilled(temporalidadeModel.Descricao);
            DocumentoFilled(temporalidadeModel.Documento);
            PrazoGuardaFilled(temporalidadeModel.PrazoGuardaFaseCorrente, temporalidadeModel.UnidadePrazoGuardaFaseCorrente, temporalidadeModel.EventoFimFaseCorrente);
            PrazoGuardaFilled(temporalidadeModel.PrazoGuardaFaseIntermediaria, temporalidadeModel.UnidadePrazoGuardaFaseIntermediaria, temporalidadeModel.EventoFimFaseIntermediaria);
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
        
        private void PrazoGuardaFilled(int? prazoGuarda, UnidadeTempoModel? unidadePrazoGuarda, string eventoFim)
        {
            if (!prazoGuarda.HasValue && string.IsNullOrWhiteSpace(eventoFim))
                throw new ScdException("Ou Prazo de Guarda ou o Evento Fim deve ser preenchido.");

            if (prazoGuarda.HasValue && !string.IsNullOrWhiteSpace(eventoFim))
                throw new ScdException("Ou Prazo de Guarda ou o Evento Fim deve ser preenchido.");

            if (prazoGuarda.HasValue && !unidadePrazoGuarda.HasValue)
                throw new ScdException("A Unidade do Prazo de Guarda deve ser preenchida quando o Prazo de Guarda é preenchido.");
        }

        private void DocumentoFilled(DocumentoModel documentoModel)
        {
            if (documentoModel.Id == default(int))
                throw new ScdException("Identificador do Documento inválido.");
        }
        #endregion

        internal async Task DocumentoExists(DocumentoModel documentoModel)
        {
            documentoModel = await _documentos.SearchAsync(documentoModel.Id);

            if (documentoModel == null)
                throw new ScdException("Documento não encontrado.");
        }
        #endregion

        internal void Found(TemporalidadeModel temporalidadeModel)
        {
            if (temporalidadeModel == null)
                throw new ScdException("Temporalidade não encontrado.");
        }

        internal async Task PlanoClassificacaoEquals(TemporalidadeModel temporalidadeModelNew, TemporalidadeModel temporalidadeModelOld)
        {
            PlanoClassificacaoModel planoClassificacaoModelNew = await _planosClassificacao.SearchByDocumentoAsync(temporalidadeModelNew.Documento.Id);

            PlanoClassificacaoModel planoClassificacaoModelOld = await _planosClassificacao.SearchByDocumentoAsync(temporalidadeModelOld.Documento.Id);

            if (planoClassificacaoModelNew.Id != planoClassificacaoModelOld.Id)
                throw new ScdException("O Plano de Classificação não pode ser alterado.");
        }

        internal async Task CanInsert(TemporalidadeModel temporalidadeModel)
        {
            PlanoClassificacaoModel planoClassificacaoModelOld = await _planosClassificacao.SearchByDocumentoAsync(temporalidadeModel.Documento.Id);

            _planoClassificacaoValidation.CanUpdate(planoClassificacaoModelOld);
        }

        internal async Task CanUpdate(TemporalidadeModel temporalidadeModel)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await _planosClassificacao.SearchByDocumentoAsync(temporalidadeModel.Documento.Id); ;

            _planoClassificacaoValidation.CanUpdate(planoClassificacaoModel);
        }

        internal async Task CanDelete(TemporalidadeModel temporalidadeModel)
        {
            await CanUpdate(temporalidadeModel);
        }
    }
}