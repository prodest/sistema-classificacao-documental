using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System.Collections.Generic;
using System.Linq;

namespace Prodest.Scd.Business
{
    public class PlanoClassificacaoCore : IPlanoClassificacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<PlanoClassificacao> _planosClassificacao;

        public PlanoClassificacaoCore(IScdRepositories repositories)
        {
            _unitOfWork = repositories.UnitOfWork;
            _planosClassificacao = repositories.PlanosClassificacao;
        }

        public List<PlanoClassificacaoModel> Listar()
        {
            List<PlanoClassificacao> planosClassificacao = _planosClassificacao.ToList();

            return new List<PlanoClassificacaoModel>();
        }
    }
}
