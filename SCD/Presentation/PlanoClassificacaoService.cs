using System;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prodest.Scd.Business.Model;
using AutoMapper;

namespace Prodest.Scd.Presentation
{
    public class PlanoClassificacaoService : IPlanoClassificacaoService
    {
        private IPlanoClassificacaoCore _core;
        private IMapper _mapper;

        public PlanoClassificacaoService(IPlanoClassificacaoCore core, IMapper mapper)
        {
            _core = core;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
        }

        public PlanoClassificacaoViewModel Insert(PlanoClassificacaoViewModel planoClassificacao)
        {
            planoClassificacao.Id = 1;

            return planoClassificacao;
        }

        public async Task<List<PlanoClassificacaoViewModel>> Search(string guidOrganizacao)
        {
            List<PlanoClassificacaoModel> planosClassificacao = await _core.SearchAsync(Environment.GetEnvironmentVariable("GuidProdest"), 1, 20);

            List<PlanoClassificacaoViewModel> planosClassificacaoViewModel = _mapper.Map<List<PlanoClassificacaoViewModel>>(planosClassificacao);

            return planosClassificacaoViewModel;
        }

        public void Update(PlanoClassificacaoViewModel planoClassificacao)
        {
        }
    }
}
