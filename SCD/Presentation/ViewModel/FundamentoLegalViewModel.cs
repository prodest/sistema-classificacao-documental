using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class FundamentoLegalViewModel : BaseViewModel
    {
        public List<FundamentoLegalEntidade> entidades { get; set; }
        public FundamentoLegalEntidade entidade { get; set; }
        public FiltroPlanoClassificacao filtro { get; set; }
    }

        public class FiltroFundamentoLegal
    {
        public string pesquisa { get; set; }
    }

    public class FundamentoLegalEntidade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Obrigatório")]



        //public bool Ativo { get; set; }

        //public string AtivoDescricao
        //{
        //    get
        //    {
        //        return Ativo ? "Ativo" : "Inativo";
        //    }
        //}

    }

}
