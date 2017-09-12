using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;
using Prodest.Scd.Persistence.Model;

namespace Prodest.Scd.Business.Validation
{
    public class TipoDocumentalValidation : CommonValidation
    {
        //private IGenericRepository<TipoDocumental>  _tiposDocumental;

        //public TipoDocumentalValidation(IScdRepositories repositories)
        //{
        //    _tiposDocumental = repositories.TiposDocumentais;
        //}

        //#region Valid
        //internal void Valid(TipoDocumentalModel tipoDocumental)
        //{
        //    BasicValid(tipoDocumental);

        //    IdValid(tipoDocumental.Id);
        //}

        //internal void IdInsertValid(int id)
        //{
        //    if (id != default(int))
        //        throw new ScdException("O id não deve ser preenchido.");
        //}

        //#endregion

        #region Basic Valid
        internal void BasicValid(TipoDocumentalModel tipoDocumental)
        {
            NotNull(tipoDocumental);

            //Filled(tipoDocumental);

            //OrganizacaoNotNull(tipoDocumental.Organizacao);
        }

        private void NotNull(TipoDocumentalModel tipoDocumental)
        {
            if (tipoDocumental == null)
                throw new ScdException("O Tipo Documental não pode ser nulo.");
        }

        //#region Filled
        //internal void Filled(TipoDocumentalModel tipoDocumental)
        //{
        //    DescricaoFilled(tipoDocumental.Descricao);
        //}

        //private void DescricaoFilled(string descricao)
        //{
        //    if (string.IsNullOrWhiteSpace(descricao) || string.IsNullOrWhiteSpace(descricao.Trim()))
        //        throw new ScdException("A descrição não pode ser vazia ou nula.");
        //}

        //private void OrganizacaoNotNull(OrganizacaoModel organizacao)
        //{
        //    if (organizacao == null)
        //        throw new ScdException("A organização não pode ser nula.");
        //}
        //#endregion
        #endregion

        internal void Found(TipoDocumental tipoDocumental)
        {
            if (tipoDocumental == null)
                throw new ScdException("Tipo Documental não encontrado.");
        }
        internal void CanDelete(TipoDocumental tipoDocumental)
        {
            //TODO: Após a inserção de Itens de Plano de Classificação verificar se existem algum associado
            //if (tipoDocumental.ItensPlanoClassificacao != null && tipoDocumental.ItensPlanoClassificacao.Count > 0)
            //    throw new ScdException("O Nivel de Classificação possui itens e não pode ser excluído.");
        }

        //internal void PaginationSearch(int page, int count)
        //{
        //    if (page <= 0)
        //        throw new ScdException("Página inválida.");

        //    if (count <= 0)
        //        throw new ScdException("Quantidade de rgistro por página inválida.");
        //}

        //internal void CanUpdate(TipoDocumentalModel newTipoDocumentalModel, TipoDocumental oldTipoDocumental)
        //{
        //    if (newTipoDocumentalModel.Organizacao != null && (oldTipoDocumental.Organizacao.Id != newTipoDocumentalModel.Organizacao.Id || !oldTipoDocumental.Organizacao.GuidOrganizacao.Equals(newTipoDocumentalModel.Organizacao.GuidOrganizacao)))
        //    {
        //        throw new ScdException("Não é possível atualizar a Organização do Nível de Classificação.");
        //    }
        //}
    }
}
