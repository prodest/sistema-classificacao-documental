using Prodest.Scd.Business.Model;
using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Prodest.Scd.Business.Model.TermoClassificacaoInformacaoModel;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class TermoClassificacaoInformacaoViewModel : BaseViewModel
    {
        public PlanoClassificacaoEntidade plano { get; set; }
        public List<TermoClassificacaoInformacaoEntidade> entidades { get; set; }
        public TermoClassificacaoInformacaoEntidade entidade { get; set; }
        public ICollection<CriterioRestricaoEntidade> Criterios { get; set; }
        public FiltroTermoClassificacaoInformacao filtro { get; set; }
        public ICollection<EnumModel> graus { get; set; }
        public ICollection<EnumModel> unidadesTempo { get; set; }
        public ICollection<EnumModel> tiposSigilo { get; set; }
    }

    public class FiltroTermoClassificacaoInformacao
    {
        public string pesquisa { get; set; }
        public int IdPlanoClassificacao { get; set; }

    }

    public class TermoClassificacaoInformacaoEntidade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Obrigatório")]



        public GrauSigiloModel GrauSigilo { get; set; }
        public string GrauDescricao
        {
            get
            {
                return GrauSigilo > 0 ? GrauSigilo.ToString() : "Não informado";
            }
        }
        public TipoSigiloModel TipoSigilo { get; set; }
        public string TipoSigiloDescricao
        {
            get
            {
                return TipoSigilo > 0 ? TipoSigilo.ToString() : "Não informado";
            }
        }
        public string ConteudoSigilo { get; set; }
        public CriterioRestricaoEntidade CriterioRestricao { get; set; }
        public DocumentoEntidade Documento { get; set; }
        public string IdentificadorDocumento { get; set; }
        public string FundamentoLegal { get; set; }
        public string Justificativa { get; set; }

        public string CpfIndicacaoAprovador { get; set; }

        public int? PrazoSigilo { get; set; }
        public UnidadeTempo? UnidadePrazoSigilo { get; set; }
        public int? IdUnidadePrazoSigilo
        {
            get
            {
                if (UnidadePrazoSigilo != null)
                {
                    return (int)UnidadePrazoSigilo;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                {
                    UnidadePrazoSigilo = (UnidadeTempo)value.Value;
                }
                else
                {
                    UnidadePrazoSigilo = null;
                }
            }
        }

        public int IdGrau
        {
            get
            {
                return (int)GrauSigilo;
            }
            set
            {
                GrauSigilo = (GrauSigiloModel)value;
            }
        }

        public int IdTipoSigilo
        {
            get
            {
                return (int)TipoSigilo;
            }
            set
            {
                TipoSigilo = (TipoSigiloModel)value;
            }
        }

        [DataType(DataType.Date)]
        public DateTime? DataProducaoDocumento { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataClassificacao { get; set; }
        public string DataProducaoDocumentoDescricao
        {
            get
            {
                return DataProducaoDocumento.HasValue ? DataProducaoDocumento.Value.ToString("dd/MM/yyyy") : "-";
            }
        }
        public string DataClassificacaoDescricao
        {
            get
            {
                return DataClassificacao.HasValue ? DataClassificacao.Value.ToString("dd/MM/yyyy") : "-";
            }
        }


    }

}
