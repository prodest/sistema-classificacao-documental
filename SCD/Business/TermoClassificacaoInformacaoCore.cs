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
            return new TermoClassificacaoInformacaoModel()
            {
                Id = 2,
                Codigo = "ABCDEFGHIJK",
                ConteudoSigilo = "ABCDEFGHIJK",
                CpfIndicacaoAprovador = "ABCDEFGHIJK",
                CpfUsuario = "ABCDEFGHIJK",
                CriterioRestricao = new CriterioRestricaoModel { Codigo = "ABCDEFGHIJK", Descricao = "ABCDEFGHIJK" },
                DataClassificacao = DateTime.Now,
                DataProducaoDocumento = DateTime.Now,
                Documento = new DocumentoModel { Codigo = "ABCDEFGHIJK", Descricao = "ABCDEFGHIJK" },
                FundamentoLegal = "ABCDEFGHIJK",
                GrauSigilo = GrauSigiloModel.Reservado,
                IdentificadorDocumento = "ABCDEFGHIJK",
                Justificativa = "ABCDEFGHIJK",
                TipoSigilo = TermoClassificacaoInformacaoModel.TipoSigiloModel.Parcial
            };
        }

        public async Task<TermoClassificacaoInformacaoModel> SearchAsync(int id)
        {
            return new TermoClassificacaoInformacaoModel()
            {
                Id = 2,
                Codigo = "ABCDEFGHIJK",
                ConteudoSigilo = "ABCDEFGHIJK",
                CpfIndicacaoAprovador = "ABCDEFGHIJK",
                CpfUsuario = "ABCDEFGHIJK",
                CriterioRestricao = new CriterioRestricaoModel { Codigo = "ABCDEFGHIJK", Descricao = "ABCDEFGHIJK" },
                DataClassificacao = DateTime.Now,
                DataProducaoDocumento = DateTime.Now,
                Documento = new DocumentoModel { Codigo = "ABCDEFGHIJK", Descricao = "ABCDEFGHIJK" },
                FundamentoLegal = "ABCDEFGHIJK",
                GrauSigilo = GrauSigiloModel.Reservado,
                IdentificadorDocumento = "ABCDEFGHIJK",
                Justificativa = "ABCDEFGHIJK",
                TipoSigilo = TermoClassificacaoInformacaoModel.TipoSigiloModel.Parcial
            };
        }

        public async Task<ICollection<TermoClassificacaoInformacaoModel>> SearchByUserAsync()
        {
            List<TermoClassificacaoInformacaoModel> retorno = new List<TermoClassificacaoInformacaoModel>
            {
                new TermoClassificacaoInformacaoModel()
                {
                    Id = 1,
                    Codigo = "ABCDEFGHIJK",
                    ConteudoSigilo = "ABCDEFGHIJK",
                    CpfIndicacaoAprovador = "ABCDEFGHIJK",
                    CpfUsuario = "ABCDEFGHIJK",
                    CriterioRestricao = new CriterioRestricaoModel { Codigo = "ABCDEFGHIJK",Descricao= "ABCDEFGHIJK"},
                    DataClassificacao = DateTime.Now,
                    DataProducaoDocumento= DateTime.Now,
                    Documento = new DocumentoModel { Codigo = "ABCDEFGHIJK",Descricao= "ABCDEFGHIJK"},
                    FundamentoLegal= "ABCDEFGHIJK",
                    GrauSigilo = GrauSigiloModel.Reservado,
                    IdentificadorDocumento= "ABCDEFGHIJK",
                    Justificativa= "ABCDEFGHIJK",
                    TipoSigilo = TermoClassificacaoInformacaoModel.TipoSigiloModel.Parcial
                },
                new TermoClassificacaoInformacaoModel()
                {
                    Id = 2,
                    Codigo = "ABCDEFGHIJK",
                    ConteudoSigilo = "ABCDEFGHIJK",
                    CpfIndicacaoAprovador = "ABCDEFGHIJK",
                    CpfUsuario = "ABCDEFGHIJK",
                    CriterioRestricao = new CriterioRestricaoModel { Codigo = "ABCDEFGHIJK",Descricao= "ABCDEFGHIJK"},
                    DataClassificacao = DateTime.Now,
                    DataProducaoDocumento= DateTime.Now,
                    Documento = new DocumentoModel { Codigo = "ABCDEFGHIJK",Descricao= "ABCDEFGHIJK"},
                    FundamentoLegal= "ABCDEFGHIJK",
                    GrauSigilo = GrauSigiloModel.Reservado,
                    IdentificadorDocumento= "ABCDEFGHIJK",
                    Justificativa= "ABCDEFGHIJK",
                    TipoSigilo = TermoClassificacaoInformacaoModel.TipoSigiloModel.Parcial
                }
            };
            return retorno;
        }

        public async Task UpdateAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
            
        }
    }
}