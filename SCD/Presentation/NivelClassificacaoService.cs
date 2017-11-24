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
    public class NivelClassificacaoService : INivelClassificacaoService
    {
        private INivelClassificacaoCore _core;
        private IMapper _mapper;
        private IOrganogramaService _organogramaService;
        public NivelClassificacaoService(INivelClassificacaoCore core, IMapper mapper, IOrganogramaService organogramaService)
        {
            _core = core;
            _mapper = mapper;
            _organogramaService = organogramaService;
        }

        public async Task<NivelClassificacaoViewModel> Delete(int id)
        {
            var model = new NivelClassificacaoViewModel();
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

        public async Task<NivelClassificacaoViewModel> Edit(int id)
        {
            var model = new NivelClassificacaoViewModel();
            try
            {
                model.Action = "Update";
                model.entidade = _mapper.Map<NivelClassificacaoEntidade>(await _core.SearchAsync(id));
                model.organizacoes = await _organogramaService.SearchAsync();
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

        public async Task<NivelClassificacaoViewModel> Update(NivelClassificacaoEntidade entidade)
        {
            var model = new NivelClassificacaoViewModel();
            model.entidade = entidade;
            try
            {
                await _core.UpdateAsync(_mapper.Map<NivelClassificacaoModel>(entidade));
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
        public async Task<NivelClassificacaoViewModel> Create(NivelClassificacaoEntidade entidade)
        {
            var model = new NivelClassificacaoViewModel();
            try
            {
                await _core.InsertAsync(_mapper.Map<NivelClassificacaoModel>(entidade));
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

        public async Task<NivelClassificacaoViewModel> Search(FiltroNivelClassificacao filtro)
        {
            //prodest
            //var guid = "3ca6ea0e-ca14-46fa-a911-22e616303722";
            //GEES
            var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
            var entidades = await _core.SearchAsync(guid, 1, 1000);
            var model = new NivelClassificacaoViewModel();
            model.entidades = _mapper.Map<List<NivelClassificacaoEntidade>>(entidades);
            model.Result = new ResultViewModel
            {
                Ok = true
            };
            return model;
        }


        public async Task<NivelClassificacaoViewModel> New()
        {
            var model = new NivelClassificacaoViewModel
            {
                Action = "Create",
                entidade = new NivelClassificacaoEntidade(),
                organizacoes = await _organogramaService.SearchAsync()
            };
            return model;
        }

       
    }
}
