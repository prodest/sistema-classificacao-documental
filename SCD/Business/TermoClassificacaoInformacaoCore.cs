using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Business.Validation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Prodest.Scd.Business
{
    public class TermoClassificacaoInformacaoCore : ITermoClassificacaoInformacaoCore
    {
        private ITermoClassificacaoInformacaoRepository _termosClassificacaoInformacao;

        private TermoClassificacaoInformacaoValidation _validation;

        public TermoClassificacaoInformacaoCore(IScdRepositories repositories, TermoClassificacaoInformacaoValidation validation)
        {
            _termosClassificacaoInformacao = repositories.TermosClassificacaoInformacaoSpecific;

            _validation = validation;
        }

        public async Task<TermoClassificacaoInformacaoModel> InsertAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            await _validation.BasicValid(termoClassificacaoInformacaoModel);

            _validation.IdInsertValid(termoClassificacaoInformacaoModel.Id);

            termoClassificacaoInformacaoModel.DataClassificacao = DateTime.Now;
            //TODO: Obter o CPF a partir do usuário logado.
            termoClassificacaoInformacaoModel.GuidOrganizacao = GetGuidOrganizacao();
            termoClassificacaoInformacaoModel.CpfUsuario = GetCpfUsuario();

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário
            termoClassificacaoInformacaoModel = await _termosClassificacaoInformacao.AddAsync(termoClassificacaoInformacaoModel);

            return termoClassificacaoInformacaoModel;
        }

        public async Task<TermoClassificacaoInformacaoModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await _termosClassificacaoInformacao.SearchAsync(id);

            _validation.Found(termoClassificacaoInformacaoModel);

            return termoClassificacaoInformacaoModel;
        }

        public async Task<ICollection<TermoClassificacaoInformacaoModel>> SearchByUserAsync()
        {
            string cpfUsuario = GetCpfUsuario();

            ICollection<TermoClassificacaoInformacaoModel> termoClassificacaoInformacaoModel = await _termosClassificacaoInformacao.SearchByUserAsync(cpfUsuario);

            return termoClassificacaoInformacaoModel;
        }

        public async Task UpdateAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            await _validation.Valid(termoClassificacaoInformacaoModel);

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModelOld = await _termosClassificacaoInformacao.SearchAsync(termoClassificacaoInformacaoModel.Id);

            _validation.Found(termoClassificacaoInformacaoModelOld);

            //TODO: Após a aprovação não poderá sofrer atualização.
            //await _validation.PlanoClassificacaoEquals(termoClassificacaoInformacaoModel, termoClassificacaoInformacaoModelOld);

            //await _validation.CanUpdate(termoClassificacaoInformacaoModelOld);

            await _termosClassificacaoInformacao.UpdateAsync(termoClassificacaoInformacaoModel);
        }

        public async Task DeleteAsync(int id)
        {
            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = await _termosClassificacaoInformacao.SearchAsync(id);

            _validation.Found(termoClassificacaoInformacaoModel);

            await _validation.CanDelete(termoClassificacaoInformacaoModel);

            await _termosClassificacaoInformacao.RemoveAsync(termoClassificacaoInformacaoModel.Id);
        }

        private Guid GetGuidOrganizacao()
        {
            //TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            Guid guidProdest = new Guid(Environment.GetEnvironmentVariable("guidProdest"));

            return guidProdest;
        }

        private string GetCpfUsuario()
        {
            return "22222222222";
        }

    }
}