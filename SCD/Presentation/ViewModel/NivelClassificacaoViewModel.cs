using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class NivelClassificacaoViewModel : BaseViewModel
    {
        public List<NivelClassificacaoEntidade> entidades { get; set; }
        public List<OrganogramaOrganizacao> organizacoes { get; set; }
        public NivelClassificacaoEntidade entidade { get; set; }
        public FiltroPlanoClassificacao filtro { get; set; }
    }

        public class FiltroNivelClassificacao
    {
        public string pesquisa { get; set; }
    }

    public class NivelClassificacaoEntidade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public bool Ativo { get; set; }

        public string AtivoDescricao
        {
            get
            {
                return Ativo ? "Ativo" : "Inativo";
            }
        }
        [Required(ErrorMessage = "Obrigatório")]
        public Guid GuidOrganizacao { get; set; }
        public string OrganizacaoDescricao { get; set; }

     

    }

}
