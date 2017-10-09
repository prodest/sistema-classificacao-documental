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
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation
{
    public class ItemPlanoClassificacaoService : IItemPlanoClassificacaoService
    {
        private IItemPlanoClassificacaoCore _core;
        private IMapper _mapper;
        private IOrganogramaService _organogramaService;

        public ItemPlanoClassificacaoService(IItemPlanoClassificacaoCore core, IMapper mapper, IOrganogramaService organogramaService)
        {
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
                            Type = TypeMessageViewModel.Sucess
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
                model.entidade = _mapper.Map<ItemPlanoClassificacaoEntidade>(_core.Search(id));
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
                            Type = TypeMessageViewModel.Sucess
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
                await _core.InsertAsync(_mapper.Map<ItemPlanoClassificacaoModel>(entidade));
                model.Result = new ResultViewModel
                {
                    Ok = true,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = "Item criado com sucesso!",
                            Type = TypeMessageViewModel.Sucess
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

        public async Task<ItemPlanoClassificacaoViewModel> Search(FiltroItemPlanoClassificacao filtro)
        {
            //prodest
            var guid = new Guid("3ca6ea0e-ca14-46fa-a911-22e616303722");
            //GEES
            //var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
            var entidades = _core.Search(guid, 1, 1000);
            var model = new ItemPlanoClassificacaoViewModel();
            model.entidades = _mapper.Map<List<ItemPlanoClassificacaoEntidade>>(entidades);
            model.Result = new ResultViewModel
            {
                Ok = true
            };
            return model;
        }

        public async Task<ItemPlanoClassificacaoViewModel> New()
        {
            var model = new ItemPlanoClassificacaoViewModel
            {
                Action = "Create",
                entidade = new ItemPlanoClassificacaoEntidade(),
            };
            return model;
        }

 
       

    }
}
