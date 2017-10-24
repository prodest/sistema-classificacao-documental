using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class ItemPlanoClassificacaoViewModel : BaseViewModel
    {
        public PlanoClassificacaoEntidade plano { get; set; }
        public ICollection<ItemPlanoClassificacaoEntidade> entidades { get; set; }
        public ICollection<NivelClassificacaoEntidade> niveis { get; set; }
        public ItemPlanoClassificacaoEntidade entidade { get; set; }
        public FiltroItemPlanoClassificacao filtro { get; set; }
      
    }

    public class FiltroItemPlanoClassificacao
    {
        public string pesquisa { get; set; }
        public int IdPlanoClassificacao { get; set; }
    }

    public class ItemPlanoClassificacaoEntidade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }

        public NivelClassificacaoEntidade NivelClassificacao { get; set; }
        public PlanoClassificacaoEntidade PlanoClassificacao { get; set; }
        public ICollection<ItemPlanoClassificacaoEntidade> ItensPlanoClassificacaoChildren { get; set; }
        public ItemPlanoClassificacaoEntidade ItemPlanoClassificacaoParent { get; set; }


        public int NivelEspacamento { get; set; }

    }

}
