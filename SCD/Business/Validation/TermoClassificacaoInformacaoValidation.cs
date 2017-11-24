using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using System.Threading.Tasks;
using System;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Repository;

namespace Prodest.Scd.Business.Validation
{
    public class TermoClassificacaoInformacaoValidation : CommonValidation
    {
        private IItemPlanoClassificacaoRepository _itensPlanoClassificacao;
        private ITipoDocumentalRepository _tiposDocumentais;

        private PlanoClassificacaoValidation _planoClassificacaoValidation;

        public TermoClassificacaoInformacaoValidation(IScdRepositories repositories, IItemPlanoClassificacaoCore itemPlanoClassificacaoCore, ITipoDocumentalCore tipoDocumentalCore, PlanoClassificacaoValidation planoClassificacaoValidation)
        {

            _itensPlanoClassificacao = repositories.ItensPlanoClassificacaoSpecific;
            _tiposDocumentais = repositories.TiposDocumentaisSpecific;

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

            ItemPlanoClassificacaoNotNull(termoClassificacaoInformacaoModel.ItemPlanoClassificacao);

            //TipoDocumentalNotNull(termoClassificacaoInformacaoModel.);

            Filled(termoClassificacaoInformacaoModel);

            await ItemPlanoClassificacaoExists(termoClassificacaoInformacaoModel.ItemPlanoClassificacao);

            //await TipoDocumentalExists(termoClassificacaoInformacaoModel.TipoDocumental);

        }

        private void NotNull(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            if (termoClassificacaoInformacaoModel == null)
                throw new ScdException("O TermoClassificacaoInformacaodo do Item do Plano de Classificação não pode ser nulo.");
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
        internal void Filled(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            CodigoFilled(termoClassificacaoInformacaoModel.Codigo);
            DescricaoFilled(termoClassificacaoInformacaoModel.Descricao);
            ItemPlanoClassificacaoFilled(termoClassificacaoInformacaoModel.ItemPlanoClassificacao);
            TipoDocumentalFilled(termoClassificacaoInformacaoModel.TipoDocumental);
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
            itemPlanoClassificacaoModel = await _itensPlanoClassificacao.SearchAsync(itemPlanoClassificacaoModel.Id);

            if (itemPlanoClassificacaoModel == null)
                throw new ScdException("Item do Plano de Classificação não encontrado.");
        }

        internal async Task TipoDocumentalExists(TipoDocumentalModel tipoDocumentalModel)
        {
            tipoDocumentalModel = await _tiposDocumentais.SearchAsync(tipoDocumentalModel.Id);

            if (tipoDocumentalModel == null)
                throw new ScdException("Tipo Documental não encontrado.");
        }
        #endregion

        internal void Found(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            if (termoClassificacaoInformacaoModel == null)
                throw new ScdException("TermoClassificacaoInformacao não encontrado.");
        }

        internal async Task PlanoClassificacaoEquals(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelNew, TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelOld)
        {
            PlanoClassificacaoModel planoClassificacaoModelNew = (await _itensPlanoClassificacao.SearchAsync(termoClassificacaoInformacaoModelNew.ItemPlanoClassificacao.Id)).PlanoClassificacao;
            PlanoClassificacaoModel planoClassificacaoModelOld = (await _itensPlanoClassificacao.SearchAsync(termoClassificacaoInformacaoModelOld.ItemPlanoClassificacao.Id)).PlanoClassificacao;

            if (planoClassificacaoModelNew.Id != planoClassificacaoModelOld.Id)
                throw new ScdException("O Plano de Classificação não pode ser alterado.");
        }

        internal async Task CanUpdate(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelOld)
        {
            PlanoClassificacaoModel planoClassificacaoModelOld = (await _itensPlanoClassificacao.SearchAsync(termoClassificacaoInformacaoModelOld.ItemPlanoClassificacao.Id)).PlanoClassificacao;

            _planoClassificacaoValidation.CanUpdate(planoClassificacaoModelOld);
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