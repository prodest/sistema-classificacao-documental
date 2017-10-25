using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using System.Threading.Tasks;
using System;

namespace Prodest.Scd.Business.Validation
{
    public class DocumentoValidation : CommonValidation
    {
        private IItemPlanoClassificacaoCore _itemPlanoClassificacaoCore;
        private IPlanoClassificacaoCore _planoClassificacaoCore;
        private ITipoDocumentalCore _tipoDocumentalCore;

        private PlanoClassificacaoValidation _planoClassificacaoValidation;

        public DocumentoValidation(IItemPlanoClassificacaoCore itemPlanoClassificacaoCore, IPlanoClassificacaoCore planoClassificacaoCore, ITipoDocumentalCore tipoDocumentalCore, PlanoClassificacaoValidation planoClassificacaoValidation)
        {
            _itemPlanoClassificacaoCore = itemPlanoClassificacaoCore;
            _planoClassificacaoCore = planoClassificacaoCore;
            _tipoDocumentalCore = tipoDocumentalCore;

            _planoClassificacaoValidation = planoClassificacaoValidation;
        }

        internal async Task Valid(DocumentoModel documentoModel)
        {
            await BasicValid(documentoModel);

            IdValid(documentoModel.Id);
        }

        #region Basic Valid
        internal async Task BasicValid(DocumentoModel documentoModel)
        {
            NotNull(documentoModel);

            ItemPlanoClassificacaoNotNull(documentoModel.ItemPlanoClassificacao);

            TipoDocumentalNotNull(documentoModel.TipoDocumental);

            Filled(documentoModel);

            await ItemPlanoClassificacaoExists(documentoModel.ItemPlanoClassificacao);

            await TipoDocumentalExists(documentoModel.TipoDocumental);

        }

        private void NotNull(DocumentoModel documentoModel)
        {
            if (documentoModel == null)
                throw new ScdException("O Documentodo do Item do Plano de Classificação não pode ser nulo.");
        }

        private void ItemPlanoClassificacaoNotNull(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            if (itemPlanoClassificacaoModel == null)
                throw new ScdException("O Item do Plano de Classificação não pode ser nulo.");
        }

        private void TipoDocumentalNotNull(TipoDocumentalModel tipoDocumentalModel)
        {
            if (tipoDocumentalModel == null)
                throw new ScdException("O Tipo Documental não pode ser nulo.");
        }

        #region Filled
        internal void Filled(DocumentoModel documentoModel)
        {
            CodigoFilled(documentoModel.Codigo);
            DescricaoFilled(documentoModel.Descricao);
            ItemPlanoClassificacaoFilled(documentoModel.ItemPlanoClassificacao);
            TipoDocumentalFilled(documentoModel.TipoDocumental);
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

        private void ItemPlanoClassificacaoFilled(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            if (itemPlanoClassificacaoModel.Id == default(int))
                throw new ScdException("Identificador do Item do Plano de Classificação inválido.");
        }

        private void TipoDocumentalFilled(TipoDocumentalModel tipoDocumentalModel)
        {
            if (tipoDocumentalModel.Id == default(int))
                throw new ScdException("Identificador do Tipo Documental inválido.");
        }
        #endregion

        internal async Task ItemPlanoClassificacaoExists(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            itemPlanoClassificacaoModel = await _itemPlanoClassificacaoCore.SearchAsync(itemPlanoClassificacaoModel.Id);

            if (itemPlanoClassificacaoModel == null)
                throw new ScdException("Item do Plano de Classificação não encontrado.");
        }

        internal async Task TipoDocumentalExists(TipoDocumentalModel tipoDocumentalModel)
        {
            tipoDocumentalModel = await _tipoDocumentalCore.SearchAsync(tipoDocumentalModel.Id);

            if (tipoDocumentalModel == null)
                throw new ScdException("Tipo Documental não encontrado.");
        }
        #endregion

        internal void Found(DocumentoModel documentoModel)
        {
            if (documentoModel == null)
                throw new ScdException("Documento não encontrado.");
        }

        internal async Task PlanoClassificacaoEquals(DocumentoModel documentoModelNew, DocumentoModel documentoModelOld)
        {
            PlanoClassificacaoModel planoClassificacaoModelNew = (await _itemPlanoClassificacaoCore.SearchAsync(documentoModelNew.ItemPlanoClassificacao.Id)).PlanoClassificacao;
            PlanoClassificacaoModel planoClassificacaoModelOld = (await _itemPlanoClassificacaoCore.SearchAsync(documentoModelOld.ItemPlanoClassificacao.Id)).PlanoClassificacao;

            if (planoClassificacaoModelNew.Id != planoClassificacaoModelOld.Id)
                throw new ScdException("O Plano de Classificação não pode ser alterado.");
        }

        internal async Task CanUpdate(DocumentoModel documentoModelOld)
        {
            PlanoClassificacaoModel planoClassificacaoModelOld = (await _itemPlanoClassificacaoCore.SearchAsync(documentoModelOld.ItemPlanoClassificacao.Id)).PlanoClassificacao;

            _planoClassificacaoValidation.CanUpdate(planoClassificacaoModelOld);
        }

        internal async Task CanDelete(DocumentoModel documentoModel, int countSigilo, int countTemporalidade)
        {
            await CanUpdate(documentoModel);

            if (countSigilo > 0)
                throw new ScdException("O Documento possui Sigilo e não pode ser excluído.");

            if (countTemporalidade > 0)
                throw new ScdException("O Documento possui Temporalidade e não pode ser excluído.");
        }
    }
}