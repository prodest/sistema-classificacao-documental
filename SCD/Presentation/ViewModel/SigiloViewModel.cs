using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Prodest.Scd.Business.Model.SigiloModel;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class SigiloViewModel : BaseViewModel
    {
        public SigiloEntidade entidade { get; set; }
        public FiltroSigilo filtro { get; set; }
        public ICollection<GrauSigilo> grauSigilo { get; set; }
    }

    public class FiltroSigilo
    {
        public string pesquisa { get; set; }
        public int IdItemPlanoClassificacao { get; set; }
    }

    public class SigiloEntidade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }

        public DocumentoEntidade Documento { get; set; }

        public GrauSigilo Grau { get; set; }
        public UnidadePrazoTerminoSigilo? UnidadePrazoTermino { get; set; }

        public string EventoFim { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Justificativa { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string FundamentoLegal { get; set; }

        public bool GerarTermo { get; set; }
        public int? PrazoTermino { get; set; }


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

}
