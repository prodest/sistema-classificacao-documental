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
    public class DocumentoService : IDocumentoService
    {
        private IDocumentoCore _core;
        private IMapper _mapper;
        private IItemPlanoClassificacaoCore _coreItemPlanoClassificacao;
        private ITipoDocumentalCore _coreTipoDocumental;

        public DocumentoService(IDocumentoCore core, IMapper mapper, IItemPlanoClassificacaoCore coreItemPlanoClassificacao, ITipoDocumentalCore coreTipoDocumental)
        {
            _coreTipoDocumental = coreTipoDocumental;
            _coreItemPlanoClassificacao = coreItemPlanoClassificacao;
            _core = core;
            _mapper = mapper;
        }
 
        public async Task<DocumentoViewModel> Delete(int id)
        {
            var model = new DocumentoViewModel();
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

        public async Task<DocumentoViewModel> Edit(int id)
        {
            var model = new DocumentoViewModel();
            try
            {
                model.Action = "Update";
                var entidade = await _core.SearchAsync(id);
                entidade.ItemPlanoClassificacao = await _coreItemPlanoClassificacao.SearchAsync(entidade.ItemPlanoClassificacao.Id);
                model.entidade = _mapper.Map<DocumentoEntidade>(entidade);
                var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
                var tipos = await _coreTipoDocumental.SearchAsync(guid, 1, 1000);
                model.tipos = _mapper.Map<ICollection<TipoDocumentalEntidade>>(tipos);
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

        public async Task<DocumentoViewModel> Update(DocumentoEntidade entidade)
        {
            var model = new DocumentoViewModel();
            model.entidade = entidade;
            var item = await _coreItemPlanoClassificacao.SearchAsync(entidade.ItemPlanoClassificacao.Id);
            model.entidade.ItemPlanoClassificacao = _mapper.Map<ItemPlanoClassificacaoEntidade>(item);
            try
            {
                await _core.UpdateAsync(_mapper.Map<DocumentoModel>(entidade));

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

        public async Task<DocumentoViewModel> Create(DocumentoEntidade entidade)
        {
            var model = new DocumentoViewModel();
            try
            {
                var modelInsert = await _core.InsertAsync(_mapper.Map<DocumentoModel>(entidade));
                modelInsert.ItemPlanoClassificacao = await _coreItemPlanoClassificacao.SearchAsync(entidade.ItemPlanoClassificacao.Id);
                model.entidade = _mapper.Map<DocumentoEntidade>(modelInsert);
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


        public async Task<DocumentoViewModel> New(int IdItemPlanoClassificacao)
        {
            var model = new DocumentoViewModel();
            try
            {
                model.Action = "Create";
                model.entidade = new DocumentoEntidade();
                var item = await _coreItemPlanoClassificacao.SearchAsync(IdItemPlanoClassificacao);
                model.entidade.ItemPlanoClassificacao = _mapper.Map<ItemPlanoClassificacaoEntidade>(item);
                var guid = new Guid("fe88eb2a-a1f3-4cb1-a684-87317baf5a57");
                var tipos = await _coreTipoDocumental.SearchAsync(guid, 1, 1000);
                model.tipos = _mapper.Map<List<TipoDocumentalEntidade>>(tipos);
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
