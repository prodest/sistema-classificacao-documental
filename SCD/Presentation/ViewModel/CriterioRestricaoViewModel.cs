using Prodest.Scd.Business.Model;
using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Prodest.Scd.Business.Model.CriterioRestricaoModel;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class CriterioRestricaoViewModel : BaseViewModel
    {
        public PlanoClassificacaoEntidade plano { get; set; }
        public ICollection<DocumentoEntidade> Documentos { get; set; }
        public ICollection<FundamentoLegalEntidade> FundamentosLegais { get; set; }
        public ICollection<CriterioRestricaoEntidade> entidades { get; set; }
        public ICollection<EnumModel> unidadesTempo { get; set; }
        public ICollection<EnumModel> graus { get; set; }
        public CriterioRestricaoEntidade entidade { get; set; }
        public FiltroCriterioRestricao filtro { get; set; }

    }

    public class FiltroCriterioRestricao
    {
        public string pesquisa { get; set; }
        public int IdPlanoClassificacao { get; set; }
    }

    public class CriterioRestricaoEntidade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }

        public string CodigoDescricao
        {
            get
            {
                return $"{Codigo} - {Descricao}";
            }
        }

        public string Justificativa { get; set; }
        public FundamentoLegalEntidade FundamentoLegal { get; set; }

        //public GrauSigiloModel? Grau { get; set; }
        //public string GrauDescricao
        //{
        //    get
        //    {
        //        return Grau > 0 ? Grau.ToString() : "Não informado";
        //    }
        //}
        public bool Classificavel { get; set; }
        public string ClassificavelDescricao
        {
            get
            {
                return Classificavel ? "Sim" : "Não";
            }
        }
        public string EventoFim { get; set; }
        public int? PrazoTermino { get; set; }
        public UnidadeTempo? UnidadePrazoTermino { get; set; }
        public int? IdUnidadePrazoTermino
        {
            get
            {
                if (UnidadePrazoTermino != null)
                {
                    return (int)UnidadePrazoTermino;
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
                    UnidadePrazoTermino = (UnidadeTempo)value.Value;
                }
                else
                {
                    UnidadePrazoTermino = null;
                }
            }
        }

        //public int? IdGrau
        //{
        //    get
        //    {
        //        if (Grau != null)
        //        {
        //            return (int)Grau;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        if (value.HasValue)
        //        {
        //            Grau = (GrauSigiloModel)value.Value;
        //        }
        //        else
        //        {
        //            Grau = null;
        //        }
        //    }
        //}

        public ICollection<DocumentoEntidade> Documentos { get; set; }
        public PlanoClassificacaoEntidade PlanoClassificacao { get; set; }

    }

}
