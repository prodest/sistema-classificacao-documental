using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class DocumentoViewModel : BaseViewModel
    {
        public DocumentoEntidade entidade { get; set; }
        public ICollection<TipoDocumentalEntidade> tipos { get; set; }
        public FiltroDocumento filtro { get; set; }

    }

    public class FiltroDocumento
    {
        public string pesquisa { get; set; }
        public int IdItemPlanoClassificacao { get; set; }
    }

    public class DocumentoEntidade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }

        public ICollection<TemporalidadeEntidade> Temporalidades { get; set; }

        public TipoDocumentalEntidade TipoDocumental { get; set; }
        public ItemPlanoClassificacaoEntidade ItemPlanoClassificacao { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public int? IdTipoDocumental
        {
            get
            {
                if (TipoDocumental != null)
                {
                    return TipoDocumental.Id;
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
                    TipoDocumental = new TipoDocumentalEntidade { Id = value.Value };
                }
                else
                {
                    TipoDocumental = null;
                }
            }
        }
    }

}
