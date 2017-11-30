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
    public class FundamentoLegalService : IFundamentoLegalService
    {
        private IFundamentoLegalCore _core;
        private IMapper _mapper;
        private IOrganogramaService _organogramaService;
        public FundamentoLegalService(IFundamentoLegalCore core, IMapper mapper, IOrganogramaService organogramaService)
        {
            _core = core;
            _mapper = mapper;
            _organogramaService = organogramaService;
        }

        public async Task<FundamentoLegalViewModel> Delete(int id)
        {
            var model = new FundamentoLegalViewModel();
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

        public async Task<FundamentoLegalViewModel> Edit(int id)
        {
            var model = new FundamentoLegalViewModel();
            try
            {
                model.Action = "Update";
                model.entidade = _mapper.Map<FundamentoLegalEntidade>(await _core.SearchAsync(id));
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

        public async Task<FundamentoLegalViewModel> Update(FundamentoLegalEntidade entidade)
        {
            var model = new FundamentoLegalViewModel();
            model.entidade = entidade;
            try
            {
                await _core.UpdateAsync(_mapper.Map<FundamentoLegalModel>(entidade));
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
        public async Task<FundamentoLegalViewModel> Create(FundamentoLegalEntidade entidade)
        {
            var model = new FundamentoLegalViewModel();
            try
            {
                await _core.InsertAsync(_mapper.Map<FundamentoLegalModel>(entidade));
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

        public async Task<FundamentoLegalViewModel> Search(FiltroFundamentoLegal filtro)
        {
            //prodest
            //var guid = "3ca6ea0e-ca14-46fa-a911-22e616303722";
            //GEES
            var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
            var entidades = await _core.SearchAsync(guid, 1, 1000);
            var model = new FundamentoLegalViewModel();
            model.entidades = _mapper.Map<List<FundamentoLegalEntidade>>(entidades);
            model.Result = new ResultViewModel
            {
                Ok = true
            };
            return model;
        }


        public async Task<FundamentoLegalViewModel> New()
        {
            var model = new FundamentoLegalViewModel
            {
                Action = "Create",
                entidade = new FundamentoLegalEntidade(),
            };
            return model;
        }

       
    }
}
