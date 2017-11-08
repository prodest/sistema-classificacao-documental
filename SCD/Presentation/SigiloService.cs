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
    public class SigiloService : ISigiloService
    {
        private ISigiloCore _core;
        private IMapper _mapper;
        private IItemPlanoClassificacaoCore _coreItemPlanoClassificacao;
        private IDocumentoCore _coreDocumento;

        public SigiloService(ISigiloCore core, IMapper mapper, IItemPlanoClassificacaoCore coreItemPlanoClassificacao, IDocumentoCore coreDocumento)
        {
            _coreDocumento = coreDocumento;
            _coreItemPlanoClassificacao = coreItemPlanoClassificacao;
            _core = core;
            _mapper = mapper;
        }
 
        public async Task<SigiloViewModel> Delete(int id)
        {
            var model = new SigiloViewModel();
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

        public async Task<SigiloViewModel> Edit(int id)
        {
            var model = new SigiloViewModel();
            try
            {
                model.Action = "Update";
                var entidade = await _core.SearchAsync(id);
                entidade.Documento = await _coreDocumento.SearchAsync(entidade.Documento.Id);
                entidade.Documento.ItemPlanoClassificacao = await _coreItemPlanoClassificacao.SearchAsync(entidade.Documento.ItemPlanoClassificacao.Id);
                model.entidade = _mapper.Map<SigiloEntidade>(entidade);
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

        public async Task<SigiloViewModel> Update(SigiloEntidade entidade)
        {
            var model = new SigiloViewModel();
            model.entidade = entidade;
            var documento = await _coreDocumento.SearchAsync(entidade.Documento.Id);
            documento.ItemPlanoClassificacao = await _coreItemPlanoClassificacao.SearchAsync(documento.ItemPlanoClassificacao.Id);
            model.entidade.Documento = _mapper.Map<DocumentoEntidade>(documento);
            try
            {
                await _core.UpdateAsync(_mapper.Map<SigiloModel>(entidade));

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

        public async Task<SigiloViewModel> Create(SigiloEntidade entidade)
        {
            var model = new SigiloViewModel();
            try
            {
                var modelInsert = await _core.InsertAsync(_mapper.Map<SigiloModel>(entidade));
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


        public async Task<SigiloViewModel> New(int idDocumento)
        {
            var model = new SigiloViewModel();
            try
            {
                model.Action = "Create";
                model.entidade = new SigiloEntidade();

                var documento = await _coreDocumento.SearchAsync(idDocumento);
                documento.ItemPlanoClassificacao = await _coreItemPlanoClassificacao.SearchAsync(documento.ItemPlanoClassificacao.Id);
                model.entidade.Documento = _mapper.Map<DocumentoEntidade>(documento);
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
