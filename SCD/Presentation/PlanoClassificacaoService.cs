using Prodest.Scd.Business.Base;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using System;
using System.Collections.Generic;

namespace Prodest.Scd.Presentation
{
    public class PlanoClassificacaoService : IPlanoClassificacaoService
    {
        private IPlanoClassificacaoCore _core;

        public PlanoClassificacaoService(IPlanoClassificacaoCore core)
        {
            _core = core;
        }

        public void Delete(int id)
        {

        }

        public PlanoClassificacaoEntidade Search(int id)
        {
            return new PlanoClassificacaoEntidade
            {
                Descricao = "aaaa",
                AreaFim = true,
                Aprovacao = DateTime.Now,
                FimVigencia = DateTime.Now
            };
        }

        public void Update(PlanoClassificacaoEntidade entidade)
        {

        }
        public PlanoClassificacaoEntidade Create(PlanoClassificacaoEntidade entidade) {
            return new PlanoClassificacaoEntidade();

        }
        public PlanoClassificacaoViewModel Search()
        {
            return this.Search(null);

        }

        public PlanoClassificacaoViewModel Search(Filtro filtro)
        {
            _core.Listar();
            var view = new PlanoClassificacaoViewModel();
            view.entidades = new List<PlanoClassificacaoEntidade>();
            view.entidades.Add(new PlanoClassificacaoEntidade
            {
                Descricao = "aaaa",
                AreaFim = true,
                Aprovacao = DateTime.Now,
                FimVigencia = DateTime.Now
            }
            );
            view.entidades.Add(new PlanoClassificacaoEntidade
            {
                Descricao = "bbbb",
                AreaFim = false,
                Aprovacao = DateTime.Now,
                FimVigencia = DateTime.Now
            }
            );
            view.entidades.Add(new PlanoClassificacaoEntidade
            {
                Descricao = "cccc",
                AreaFim = true,
                Aprovacao = DateTime.Now,
                FimVigencia = DateTime.Now
            }
            );
            return view;
        }




        public PlanoClassificacaoEntidade Insert(PlanoClassificacaoEntidade entidade)
        {
            return new PlanoClassificacaoEntidade();
        }
    }
}
