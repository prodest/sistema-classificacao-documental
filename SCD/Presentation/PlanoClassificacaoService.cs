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
                model.entidade = _mapper.Map<PlanoClassificacaoEntidade>(await _core.SearchAsync(id));
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
            var entidades = await _core.GetAsync(1, 1000);
            //await _core.SearchCompleteAsync(entidades.FirstOrDefault().Id);
            var model = new PlanoClassificacaoViewModel();
            model.entidades = _mapper.Map<List<PlanoClassificacaoEntidade>>(entidades);
            model.Result = new ResultViewModel
            {
                Ok = true
            };
            return model;
        }

        public PlanoClassificacaoViewModel New()
        {
            var model = new PlanoClassificacaoViewModel
            {
                Action = "Create",
                entidade = new PlanoClassificacaoEntidade(),
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
                model.entidade = _mapper.Map<PlanoClassificacaoEntidade>(await _core.SearchAsync(id));
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
