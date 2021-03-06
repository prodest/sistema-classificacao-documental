﻿using System.Collections.Generic;

namespace Prodest.Scd.Persistence.Model
{
    public partial class NivelClassificacao
    {
        public NivelClassificacao()
        {
            ItensPlanoClassificacao = new HashSet<ItemPlanoClassificacao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int IdOrganizacao { get; set; }

        public Organizacao Organizacao { get; set; }
        public ICollection<ItemPlanoClassificacao> ItensPlanoClassificacao { get; set; }
    }
}
