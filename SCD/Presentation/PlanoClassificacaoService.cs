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
    public class PlanoClassificacaoService : IPlanoClassificacaoService
    {
        private IPlanoClassificacaoCore _core;
        private IMapper _mapper;
        private IOrganogramaService _organogramaService;

        public PlanoClassificacaoService(IPlanoClassificacaoCore core, IMapper mapper, IOrganogramaService organogramaService)
        {
            _core = core;
            _mapper = mapper;
            _organogramaService = organogramaService;
        }

        public async Task<PlanoClassificacaoViewModel> Delete(int id)
        {
            var model = new PlanoClassificacaoViewModel();
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

        public async Task<PlanoClassificacaoViewModel> Edit(int id)
        {
            var model = new PlanoClassificacaoViewModel();
            try
            {
                model.Action = "Update";
                model.entidade = _mapper.Map<PlanoClassificacaoEntidade>(_core.Search(id));
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

        public async Task<PlanoClassificacaoViewModel> Update(PlanoClassificacaoEntidade entidade)
        {
            var model = new PlanoClassificacaoViewModel();
            model.entidade = entidade;
            try
            {
                await _core.UpdateAsync(_mapper.Map<PlanoClassificacaoModel>(entidade));
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

        public async Task<PlanoClassificacaoViewModel> Create(PlanoClassificacaoEntidade entidade)
        {
            var model = new PlanoClassificacaoViewModel();
            try
            {
                await _core.InsertAsync(_mapper.Map<PlanoClassificacaoModel>(entidade));
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

        public async Task<PlanoClassificacaoViewModel> Search(FiltroPlanoClassificacao filtro)
        {
            //prodest
            //var guid = "3ca6ea0e-ca14-46fa-a911-22e616303722";
            //GEES
            var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
            var entidades = _core.Search(guid, 1, 1000);
            var model = new PlanoClassificacaoViewModel();
            model.entidades = _mapper.Map<List<PlanoClassificacaoEntidade>>(entidades);
            model.Result = new ResultViewModel
            {
                Ok = true
            };
            return model;
        }

        public async Task<PlanoClassificacaoViewModel> New()
        {
            var model = new PlanoClassificacaoViewModel
            {
                Action = "Create",
                entidade = new PlanoClassificacaoEntidade(),
                organizacoes = await _organogramaService.SearchAsync()
            };
            return model;
        }

        #region Fim Vigência
            public async Task<PlanoClassificacaoViewModel> EncerrarVigencia(int id)
        {
            var model = new PlanoClassificacaoViewModel();
            try
            {
                model.Action = "UpdateVigencia";
                model.entidade = _mapper.Map<PlanoClassificacaoEntidade>(_core.Search(id));
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
            public async Task<PlanoClassificacaoViewModel> UpdateVigencia(PlanoClassificacaoEntidade entidade)
        {
            var model = new PlanoClassificacaoViewModel();
            model.entidade = entidade;
            try
            {
                await _core.UpdateFimVigenciaAsync(entidade.Id, entidade.FimVigencia.Value);
                model.Result = new ResultViewModel
                {
                    Ok = true,
                    Messages = new List<MessageViewModel>()
                    {
                        new MessageViewModel{
                            Message = "Data Fim de Vigência informada com sucesso!",
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
        #endregion
       

    }
}
