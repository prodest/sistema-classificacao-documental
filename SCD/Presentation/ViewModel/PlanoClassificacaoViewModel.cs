using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prodest.Scd.Presentation.ViewModel
{
    public class PlanoClassificacaoViewModel
    {
        public List<PlanoClassificacaoEntidade> entidades { get; set; }
        public List<Organizacao> organizacoes = new List<Organizacao> {
            new Organizacao{guid=Guid.NewGuid(),sigla="Prodest" },
            new Organizacao{guid= Guid.NewGuid(), sigla= "Seger"}
        };
        public PlanoClassificacaoEntidade entidade { get; set; }
        public string mensagem { get; set; }
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
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public bool AreaFim { get; set; }

        public string AreaFimDescricao
        {
            get
            {
                return AreaFim ? "Fim" : "Meio";
            }
        }
        [Required]
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
