using Prodest.Scd.Business.Base;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
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

        public PlanoClassificacaoViewModel Insert(PlanoClassificacaoViewModel planoClassificacao)
        {
            planoClassificacao.Id = 1;

            return planoClassificacao;
        }

        public List<PlanoClassificacaoViewModel> Search(string guidOrganizacao)
        {
            _core.SearchAsync(null);

            return new List<PlanoClassificacaoViewModel>();
        }

        public void Update(PlanoClassificacaoViewModel planoClassificacao)
        {
        }
    }
}
