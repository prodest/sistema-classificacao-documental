using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation.Common;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Validation
{
    public class SigiloValidation : CommonValidation
    {
        private IDocumentoRepository _documentos;
        private IPlanoClassificacaoRepository _planosClassificacao;

        private PlanoClassificacaoValidation _planoClassificacaoValidation;

        public SigiloValidation(IScdRepositories repositories, PlanoClassificacaoValidation planoClassificacaoValidation)
        {
            _documentos = repositories.DocumentosSpecific;
            _planosClassificacao = repositories.PlanosClassificacaoSpecific;

            _planoClassificacaoValidation = planoClassificacaoValidation;
        }

        internal async Task Valid(SigiloModel sigiloModel)
        {
            await BasicValid(sigiloModel);

            IdValid(sigiloModel.Id);
        }

        #region Basic Valid
        internal async Task BasicValid(SigiloModel sigiloModel)
        {
            NotNull(sigiloModel);

            DocumentoNotNull(sigiloModel.Documento);

            Filled(sigiloModel);

            await DocumentoExists(sigiloModel.Documento);
        }

        private void NotNull(SigiloModel sigiloModel)
        {
            if (sigiloModel == null)
                throw new ScdException("O Sigilo não pode ser nulo.");
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
        internal void Filled(SigiloModel sigiloModel)
        {
            CodigoFilled(sigiloModel.Codigo);
            DescricaoFilled(sigiloModel.Descricao);
            DocumentoFilled(sigiloModel.Documento);
            PrazoTerminoOrEventoFimFilled(sigiloModel.PrazoTermino, sigiloModel.UnidadePrazoTermino, sigiloModel.EventoFim);
            JustificativaFilled(sigiloModel.Justificativa);
            FundamentoLegalFilled(sigiloModel.FundamentoLegal);
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

        private void PrazoTerminoOrEventoFimFilled(int? prazoTermino, SigiloModel.UnidadePrazoTerminoSigilo? unidadePrazoTerminoSigilo, string eventoFim)
        {
            if (!prazoTermino.HasValue && string.IsNullOrWhiteSpace(eventoFim))
                throw new ScdException("Ou Prazo de Término ou o Evento Fim deve ser preenchido.");

            if (prazoTermino.HasValue && !string.IsNullOrWhiteSpace(eventoFim))
                throw new ScdException("Ou Prazo de Término ou o Evento Fim deve ser preenchido.");

            if (prazoTermino.HasValue && !unidadePrazoTerminoSigilo.HasValue)
                throw new ScdException("A Unidade do Prazo Término deve ser preenchida quando o Prazo de Término é preenchido.");
        }

        private void FundamentoLegalFilled(string fundamentoLegal)
        {
            if (string.IsNullOrWhiteSpace(fundamentoLegal))
                throw new ScdException("O Fundamento Legal não pode ser vazia ou nula.");
        }

        private void JustificativaFilled(string justificativa)
        {
            if (string.IsNullOrWhiteSpace(justificativa))
                throw new ScdException("A Justificativa não pode ser vazia ou nula.");
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

        internal void Found(SigiloModel sigiloModel)
        {
            if (sigiloModel == null)
                throw new ScdException("Sigilo não encontrado.");
        }

        internal async Task PlanoClassificacaoEquals(SigiloModel sigiloModelNew, SigiloModel sigiloModelOld)
        {
            PlanoClassificacaoModel planoClassificacaoModelNew = await _planosClassificacao.SearchByDocumentoAsync(sigiloModelNew.Documento.Id);

            PlanoClassificacaoModel planoClassificacaoModelOld = await _planosClassificacao.SearchByDocumentoAsync(sigiloModelOld.Documento.Id);

            if (planoClassificacaoModelNew.Id != planoClassificacaoModelOld.Id)
                throw new ScdException("O Plano de Classificação não pode ser alterado.");
        }

        internal async Task CanInsert(SigiloModel sigiloModel)
        {
            PlanoClassificacaoModel planoClassificacaoModelOld = await _planosClassificacao.SearchByDocumentoAsync(sigiloModel.Documento.Id);

            _planoClassificacaoValidation.CanUpdate(planoClassificacaoModelOld);
        }

        internal async Task CanUpdate(SigiloModel sigiloModel)
        {
            PlanoClassificacaoModel planoClassificacaoModel = await _planosClassificacao.SearchByDocumentoAsync(sigiloModel.Documento.Id); ;

            _planoClassificacaoValidation.CanUpdate(planoClassificacaoModel);
        }

        internal async Task CanDelete(SigiloModel sigiloModel)
        {
            await CanUpdate(sigiloModel);
        }
    }
}