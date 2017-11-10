using Prodest.Scd.Business.Model;
using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Prodest.Scd.Business.Model.TemporalidadeModel;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class TemporalidadeViewModel : BaseViewModel
    {
        public TemporalidadeEntidade entidade { get; set; }
        public FiltroTemporalidade filtro { get; set; }
        public ICollection<EnumModel> unidadesTempo { get; set; }
        public ICollection<EnumModel> destinacoes { get; set; }
    }

    public class FiltroTemporalidade
    {
        public string pesquisa { get; set; }
        public int IdItemPlanoClassificacao { get; set; }
    }

    public class TemporalidadeEntidade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }

        public DocumentoEntidade Documento { get; set; }

        public DestinacaoFinal DestinacaoFinal { get; set; }
        public UnidadeTempo? UnidadePrazoGuardaFaseCorrente { get; set; }
        public UnidadeTempo? UnidadePrazoGuardaFaseIntermediaria { get; set; }

        public int? IdUnidadePrazoGuardaFaseCorrente
        {
            get
            {
                if (UnidadePrazoGuardaFaseCorrente != null)
                {
                    return (int)UnidadePrazoGuardaFaseCorrente;
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
                    UnidadePrazoGuardaFaseCorrente = (UnidadeTempo)value.Value;
                }
                else
                {
                    UnidadePrazoGuardaFaseCorrente = null;
                }
            }
        }
        public int? IdUnidadePrazoGuardaFaseIntermediaria
        {
            get
            {
                if (UnidadePrazoGuardaFaseIntermediaria != null)
                {
                    return (int)UnidadePrazoGuardaFaseIntermediaria;
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
                    UnidadePrazoGuardaFaseIntermediaria = (UnidadeTempo)value.Value;
                }
                else
                {
                    UnidadePrazoGuardaFaseIntermediaria = null;
                }
            }
        }
        public int IdDestinacaoFinal
        {
            get
            {
                return (int)DestinacaoFinal;
            }
            set
            {
                DestinacaoFinal = (DestinacaoFinal)value;
            }
        }

        public string EventoFimFaseIntermediaria { get; set; }
        public string EventoFimFaseCorrente { get; set; }

        public string Observacao { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string FundamentoLegal { get; set; }


        public int? PrazoGuardaFaseCorrente { get; set; }
        public int? PrazoGuardaFaseIntermediaria { get; set; }


        [Required(ErrorMessage = "Obrigatório")]
        public int? IdDocumento
        {
            get
            {
                if (Documento != null)
                {
                    return Documento.Id;
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
                    Documento = new DocumentoEntidade { Id = value.Value };
                }
                else
                {
                    Documento = null;
                }
            }
        }
    }


    public class EnumModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
