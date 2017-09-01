﻿using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class PlanoClassificacaoViewModel : BaseViewModel
    {
        public List<PlanoClassificacaoEntidade> entidades { get; set; }
        public List<OrganogramaOrganizacao> organizacoes { get; set; }
        public PlanoClassificacaoEntidade entidade { get; set; }
        public Filtro filtro { get; set; }
    }

    public class Organizacao
    {
        public Guid guid { get; set; }
        public string sigla { get; set; }
    }

    public class Filtro
    {
        public string pesquisa { get; set; }
    }

    public class PlanoClassificacaoEntidade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public bool AreaFim { get; set; }

        public string AreaFimDescricao
        {
            get
            {
                return AreaFim ? "Fim" : "Meio";
            }
        }
        [Required(ErrorMessage = "Obrigatório")]
        public Guid GuidOrganizacao { get; set; }
        public string OrganizacaoDescricao { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Aprovacao { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Publicacao { get; set; }

        [DataType(DataType.Date)]
        public DateTime? InicioVigencia { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FimVigencia { get; set; }

        public string AprovacaoDescricao
        {
            get
            {
                return Aprovacao.HasValue ? Aprovacao.Value.ToString("dd/MM/yyyy") : "-";
            }
        }
        public string PublicacaoDescricao
        {
            get
            {
                return Publicacao.HasValue ? Publicacao.Value.ToString("dd/MM/yyyy") : "-";
            }
        }
        public string InicioVigenciaDescricao
        {
            get
            {
                return InicioVigencia.HasValue ? InicioVigencia.Value.ToString("dd/MM/yyyy") : "-";
            }
        }
        public string FimVigenciaDescricao
        {
            get
            {
                return FimVigencia.HasValue ? FimVigencia.Value.ToString("dd/MM/yyyy") : "-";
            }
        }

    }

}
