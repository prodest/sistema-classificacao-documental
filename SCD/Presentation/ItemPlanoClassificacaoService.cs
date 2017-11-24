using AutoMapper;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Integration.Organograma.Base;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation
{
    public class ItemPlanoClassificacaoService : IItemPlanoClassificacaoService
    {
        private IItemPlanoClassificacaoCore _core;
        private IPlanoClassificacaoCore _corePlanoClassificacao;
        private INivelClassificacaoCore _coreNivelClassificacao;
        private IMapper _mapper;
        private IOrganogramaService _organogramaService;

        public ItemPlanoClassificacaoService(IItemPlanoClassificacaoCore core, IPlanoClassificacaoCore corePlanoClassificacao, INivelClassificacaoCore coreNivelClassificacao, IMapper mapper, IOrganogramaService organogramaService)
        {
            _corePlanoClassificacao = corePlanoClassificacao;
            _coreNivelClassificacao = coreNivelClassificacao;
            _core = core;
            _mapper = mapper;
            _organogramaService = organogramaService;
        }

        public async Task<ItemPlanoClassificacaoViewModel> Delete(int id)
        {
            var model = new ItemPlanoClassificacaoViewModel();
            try
            {
                await _core.DeleteAsync(id);
                model.Result = new ResultViewModel
                {
                    Ok = true,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = "Item removido com sucesso!",
                            Type = TypeMessageViewModel.Success
                        }
                    }
                };
            }
            catch (ScdException e)
            {
                model.Result = new ResultViewModel
                {
                    Ok = false,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = e.Message,
                            Type = TypeMessageViewModel.Fail
                        }
                    }
                };
            }
            return model;
        }

        public async Task<ItemPlanoClassificacaoViewModel> Edit(int id)
        {
            var model = new ItemPlanoClassificacaoViewModel();
            try
            {
                model.Action = "Update";
                model.entidade = _mapper.Map<ItemPlanoClassificacaoEntidade>(await _core.SearchAsync(id));
                var entidades = await _core.SearchAsync(model.entidade.PlanoClassificacao.Id, 1, 1000);
                var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
                var niveis = await _coreNivelClassificacao.SearchAsync(guid, 1, 1000);
                model.entidades = _mapper.Map<ICollection<ItemPlanoClassificacaoEntidade>>(entidades.Where(p => p.Id != id).OrderBy(p => p.Codigo).ToList());
                model.niveis = _mapper.Map<ICollection<NivelClassificacaoEntidade>>(niveis);
                model.Result = new ResultViewModel
                {
                    Ok = true
                };
            }
            catch (ScdException e)
            {
                model.Result = new ResultViewModel
                {
                    Ok = false,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = e.Message,
                            Type = TypeMessageViewModel.Fail
                        }
                    }
                };
            }
            return model;
        }

        public async Task<ItemPlanoClassificacaoViewModel> Update(ItemPlanoClassificacaoEntidade entidade)
        {
            var model = new ItemPlanoClassificacaoViewModel();
            model.entidade = entidade;
            try
            {
                await _core.UpdateAsync(_mapper.Map<ItemPlanoClassificacaoModel>(entidade));
                model.Result = new ResultViewModel
                {
                    Ok = true,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = "Item alterado com sucesso!",
                            Type = TypeMessageViewModel.Success
                        }
                    }
                };
            }
            catch (ScdException e)
            {
                model.Result = new ResultViewModel
                {
                    Ok = false,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = e.Message,
                            Type = TypeMessageViewModel.Fail
                        }
                    }
                };
            }
            return model;
        }

        public async Task<ItemPlanoClassificacaoViewModel> Create(ItemPlanoClassificacaoEntidade entidade)
        {
            var model = new ItemPlanoClassificacaoViewModel();
            try
            {
                var modelInsert = await _core.InsertAsync(_mapper.Map<ItemPlanoClassificacaoModel>(entidade));
                model.entidade = _mapper.Map<ItemPlanoClassificacaoEntidade>(modelInsert);
                model.Result = new ResultViewModel
                {
                    Ok = true,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = "Item criado com sucesso!",
                            Type = TypeMessageViewModel.Success
                        }
                    }
                };
            }
            catch (ScdException e)
            {
                model.Result = new ResultViewModel
                {
                    Ok = false,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = e.Message,
                            Type = TypeMessageViewModel.Fail
                        }
                    }
                };
            }
            return model;
        }


        private void AtribuirNivelEspacamento(ICollection<ItemPlanoClassificacaoEntidade> entidades, int nivelPai)
        {
            foreach (var item in entidades)
            {
                item.NivelEspacamento = nivelPai + 1;
                AtribuirNivelEspacamento(item.ItensPlanoClassificacaoChildren, item.NivelEspacamento);
            }
        }

        public async Task<ItemPlanoClassificacaoViewModel> Search(FiltroItemPlanoClassificacao filtro)
        {
            var model = new ItemPlanoClassificacaoViewModel();

            var plano = await _corePlanoClassificacao.SearchCompleteAsync(filtro.IdPlanoClassificacao);
            model.plano = _mapper.Map<PlanoClassificacaoEntidade>(plano);

            

            //var entidades = await _core.SearchAsync(filtro.IdPlanoClassificacao, 1, 1000);

            ////Preciso apenas do primeiro nível, pois os filhos já estão mapeados dentro de cada item
            //entidades = entidades.Where(p => p.ItemPlanoClassificacaoParent == null).ToList();
            //var entidadesViewModel = _mapper.Map<ICollection<ItemPlanoClassificacaoEntidade>>(entidades);
            //Atribui um valor (0-99) para permitir a exibição aninhada dos itens de plano de ação conforme a estrutura cadastrada
            AtribuirNivelEspacamento(model.plano.ItensPlanoClassificacao, 0);

            model.entidades = model.plano.ItensPlanoClassificacao;
            model.Result = new ResultViewModel
            {
                Ok = true
            };
            return model;
        }

        public async Task<ItemPlanoClassificacaoViewModel> New(int idPlanoClassificacao, int? IdItemPlanoClassificacaoParent)
        {
            var model = new ItemPlanoClassificacaoViewModel();
            try
            {
                model.Action = "Create";
                model.entidade = new ItemPlanoClassificacaoEntidade {
                    PlanoClassificacao = new PlanoClassificacaoEntidade { Id = idPlanoClassificacao },
                    IdItemPlanoClassificacaoParent = IdItemPlanoClassificacaoParent
                };
                var entidades = await _core.SearchAsync(model.entidade.PlanoClassificacao.Id, 1, 1000);
                var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
                var niveis = await _coreNivelClassificacao.SearchAsync(guid, 1, 1000);
                model.entidades = _mapper.Map<ICollection<ItemPlanoClassificacaoEntidade>>(entidades.OrderBy(p => p.Codigo).ToList());
                model.niveis = _mapper.Map<ICollection<NivelClassificacaoEntidade>>(niveis);
                model.Result = new ResultViewModel
                {
                    Ok = true
                };
            }
            catch (ScdException e)
            {
                model.Result = new ResultViewModel
                {
                    Ok = false,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = e.Message,
                            Type = TypeMessageViewModel.Fail
                        }
                    }
                };
            }
            return model;
        }




    }
}
