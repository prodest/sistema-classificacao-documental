using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using System.Threading.Tasks;
using System;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Repository;
using static Prodest.Scd.Business.Model.TermoClassificacaoInformacaoModel;
using System.Linq;

namespace Prodest.Scd.Business.Validation
{
    public class TermoClassificacaoInformacaoValidation : CommonValidation
    {
        private IDocumentoRepository _documentos;
        private ICriterioRestricaoRepository _criteriosRestricao;

        private PlanoClassificacaoValidation _planoClassificacaoValidation;

        public TermoClassificacaoInformacaoValidation(IScdRepositories repositories, IItemPlanoClassificacaoCore itemPlanoClassificacaoCore, ITipoDocumentalCore tipoDocumentalCore, PlanoClassificacaoValidation planoClassificacaoValidation)
        {
            _documentos = repositories.DocumentosSpecific;
            _criteriosRestricao = repositories.CriteriosRestricaoSpecific;

            _planoClassificacaoValidation = planoClassificacaoValidation;
        }

        internal async Task Valid(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            await BasicValid(termoClassificacaoInformacaoModel);

            IdValid(termoClassificacaoInformacaoModel.Id);
        }

        #region Basic Valid
        internal async Task BasicValid(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            NotNull(termoClassificacaoInformacaoModel);

            DocumentoNotNull(termoClassificacaoInformacaoModel.Documento);
            CriterioRestricaoNotNull(termoClassificacaoInformacaoModel.CriterioRestricao);

            Filled(termoClassificacaoInformacaoModel);

            await DocumentoExists(termoClassificacaoInformacaoModel.Documento);
            await CriterioRestricaoExists(termoClassificacaoInformacaoModel.CriterioRestricao);

            await DocumentoLinkedCriterioRestricao(termoClassificacaoInformacaoModel.Documento, termoClassificacaoInformacaoModel.CriterioRestricao);
        }

        private void NotNull(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            if (termoClassificacaoInformacaoModel == null)
                throw new ScdException("O TermoClassificacaoInformacaodo do Item do Plano de Classificação não pode ser nulo.");
        }

        private void DocumentoNotNull(DocumentoModel documentoModel)
        {
            if (documentoModel == null)
                throw new ScdException("O Documento não pode ser nulo.");
        }

        private void CriterioRestricaoNotNull(CriterioRestricaoModel criterioRestricaoModel)
        {
            if (criterioRestricaoModel == null)
                throw new ScdException("O Critério de Restrição não pode ser nulo.");
        }

        #region Filled
        internal void Filled(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            CodigoFilled(termoClassificacaoInformacaoModel.Codigo);
            GrauSigiloFilled(termoClassificacaoInformacaoModel.GrauSigilo);
            TipoSigiloFilled(termoClassificacaoInformacaoModel.TipoSigilo);
            ConteudoSigiloFilled(termoClassificacaoInformacaoModel.ConteudoSigilo);
            IdentificadorDocumentoFilled(termoClassificacaoInformacaoModel.IdentificadorDocumento);
            FundamentoLegalFilled(termoClassificacaoInformacaoModel.FundamentoLegal);
            JustificativaFilled(termoClassificacaoInformacaoModel.Justificativa);
            CpfIndicacaoAprovadorFilled(termoClassificacaoInformacaoModel.CpfIndicacaoAprovador);
            PrazoSigiloFilled(termoClassificacaoInformacaoModel.PrazoSigilo);
            UnidadePrazoSigiloFilled(termoClassificacaoInformacaoModel.UnidadePrazoSigilo);

            DocumentoFilled(termoClassificacaoInformacaoModel.Documento);
            CriterioRestricaoFilled(termoClassificacaoInformacaoModel.CriterioRestricao);
        }

        private void CodigoFilled(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(codigo.Trim()))
                throw new ScdException("O código não pode ser vazio ou nulo.");
        }

        private void GrauSigiloFilled(GrauSigiloModel grauSigiloModel)
        {
            if (!Enum.IsDefined(typeof(GrauSigiloModel), grauSigiloModel))
                throw new ScdException("O Grau de Sigilo não pode ser vazio ou nulo.");
        }

        private void TipoSigiloFilled(TipoSigiloModel tipoSigiloModel)
        {
            if (!Enum.IsDefined(typeof(TipoSigiloModel), tipoSigiloModel))
                throw new ScdException("O tipo de Sigilo não pode ser vazio ou nulo.");
        }

        private void ConteudoSigiloFilled(string conteudoSigilo)
        {
            if (string.IsNullOrWhiteSpace(conteudoSigilo) || string.IsNullOrWhiteSpace(conteudoSigilo.Trim()))
                throw new ScdException("O Conteúdo do Sigilo não pode ser vazio ou nulo.");
        }

        private void IdentificadorDocumentoFilled(string identificadorDocumento)
        {
            if (string.IsNullOrWhiteSpace(identificadorDocumento) || string.IsNullOrWhiteSpace(identificadorDocumento.Trim()))
                throw new ScdException("O Identificador do Documento não pode ser vazio ou nulo.");
        }

        private void FundamentoLegalFilled(string fundamentoLegal)
        {
            if (string.IsNullOrWhiteSpace(fundamentoLegal) || string.IsNullOrWhiteSpace(fundamentoLegal.Trim()))
                throw new ScdException("O Fundamento Legal não pode ser vazio ou nulo.");
        }

        private void JustificativaFilled(string justificativa)
        {
            if (string.IsNullOrWhiteSpace(justificativa) || string.IsNullOrWhiteSpace(justificativa.Trim()))
                throw new ScdException("A Jsutificativa não pode ser vazio ou nulo.");
        }

        private void CpfIndicacaoAprovadorFilled(string cpfIndicacaoAprovador)
        {
            if (string.IsNullOrWhiteSpace(cpfIndicacaoAprovador) || string.IsNullOrWhiteSpace(cpfIndicacaoAprovador.Trim()))
                throw new ScdException("O CPF do Aprovador não pode ser vazio ou nulo.");
        }

        private void PrazoSigiloFilled(int prazoSigilo)
        {
            if (prazoSigilo <= 0)
                throw new ScdException("O Prazo do Sigilo não pode ser menor ou igual a zero.");
        }

        private void UnidadePrazoSigiloFilled(UnidadeTempo unidadePrazoSigilo)
        {
            if (!Enum.IsDefined(typeof(UnidadeTempo), unidadePrazoSigilo))
                throw new ScdException("A Unidade do Prazo do Sigilo não pode ser vazio ou nulo.");
        }

        private void DocumentoFilled(DocumentoModel documentoModel)
        {
            if (documentoModel.Id == default(int))
                throw new ScdException("Identificador do Documento inválido.");
        }

        private void CriterioRestricaoFilled(CriterioRestricaoModel criterioRestricaoModel)
        {
            if (criterioRestricaoModel.Id == default(int))
                throw new ScdException("Identificador do Critério de Restrição inválido.");
        }
        #endregion

        internal async Task DocumentoExists(DocumentoModel documentoModel)
        {
            documentoModel = await _documentos.SearchAsync(documentoModel.Id);

            if (documentoModel == null)
                throw new ScdException("Documento não encontrado.");
        }

        internal async Task CriterioRestricaoExists(CriterioRestricaoModel criterioRestricaoModel)
        {
            criterioRestricaoModel = await _criteriosRestricao.SearchAsync(criterioRestricaoModel.Id);

            if (criterioRestricaoModel == null)
                throw new ScdException("Critério de Restrição não encontrado.");
        }

        private async Task DocumentoLinkedCriterioRestricao(DocumentoModel documentoModel, CriterioRestricaoModel criterioRestricaoModel)
        {
            CriterioRestricaoModel criterioRestricaoSearchedModel = await _criteriosRestricao.SearchAsync(criterioRestricaoModel.Id);

            DocumentoModel documentoSearchedModel = criterioRestricaoModel.Documentos.SingleOrDefault(d => d.Id == documentoModel.Id);

            if (documentoSearchedModel == null)
                throw new ScdException("O Critério de Restrição não está associado ao Documento selecionado.");
        }
        #endregion

        internal void Found(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            if (termoClassificacaoInformacaoModel == null)
                throw new ScdException("TermoClassificacaoInformacao não encontrado.");
        }

        internal async Task PlanoClassificacaoEquals(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelNew, TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelOld)
        {
            //PlanoClassificacaoModel planoClassificacaoModelNew = (await _itensPlanoClassificacao.SearchAsync(termoClassificacaoInformacaoModelNew.ItemPlanoClassificacao.Id)).PlanoClassificacao;
            //PlanoClassificacaoModel planoClassificacaoModelOld = (await _itensPlanoClassificacao.SearchAsync(termoClassificacaoInformacaoModelOld.ItemPlanoClassificacao.Id)).PlanoClassificacao;

            //if (planoClassificacaoModelNew.Id != planoClassificacaoModelOld.Id)
            //    throw new ScdException("O Plano de Classificação não pode ser alterado.");
        }

        internal async Task CanUpdate(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelOld)
        {
            //PlanoClassificacaoModel planoClassificacaoModelOld = (await _itensPlanoClassificacao.SearchAsync(termoClassificacaoInformacaoModelOld.ItemPlanoClassificacao.Id)).PlanoClassificacao;

            //_planoClassificacaoValidation.CanUpdate(planoClassificacaoModelOld);
        }

        internal async Task CanDelete(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            await CanUpdate(termoClassificacaoInformacaoModel);

            //TODO: Validar se possui sigilo e temporalidade

            //if (countSigilo > 0)
            //    throw new ScdException("O TermoClassificacaoInformacao possui Sigilo e não pode ser excluído.");

            //if (countTemporalidade > 0)
            //    throw new ScdException("O TermoClassificacaoInformacao possui Temporalidade e não pode ser excluído.");
        }
    }
}