using AutoMapper;
using Prodest.Scd.Business;
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
    public class TemporalidadeService : ITemporalidadeService
    {
        private ITemporalidadeCore _core;
        private IMapper _mapper;
        private IItemPlanoClassificacaoCore _coreItemPlanoClassificacao;
        private IDocumentoCore _coreDocumento;

        public TemporalidadeService(ITemporalidadeCore core, IMapper mapper, IItemPlanoClassificacaoCore coreItemPlanoClassificacao, IDocumentoCore coreDocumento)
        {
            _coreDocumento = coreDocumento;
            _coreItemPlanoClassificacao = coreItemPlanoClassificacao;
            _core = core;
            _mapper = mapper;
        }
 
        public async Task<TemporalidadeViewModel> Delete(int id)
        {
            var model = new TemporalidadeViewModel();
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

        public async Task<TemporalidadeViewModel> Edit(int id)
        {
            var model = new TemporalidadeViewModel();
            try
            {
                model.Action = "Update";
                model.unidadesTempo = obterListaUnidadesTempo();
                model.destinacoes = obterListaDestinacao();
                var entidade = await _core.SearchAsync(id);
                entidade.Documento = await _coreDocumento.SearchAsync(entidade.Documento.Id);
                entidade.Documento.ItemPlanoClassificacao = await _coreItemPlanoClassificacao.SearchAsync(entidade.Documento.ItemPlanoClassificacao.Id);
                model.entidade = _mapper.Map<TemporalidadeEntidade>(entidade);
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

        public async Task<TemporalidadeViewModel> Update(TemporalidadeEntidade entidade)
        {
            var model = new TemporalidadeViewModel();
            model.entidade = entidade;
            var documento = await _coreDocumento.SearchAsync(entidade.Documento.Id);
            documento.ItemPlanoClassificacao = await _coreItemPlanoClassificacao.SearchAsync(documento.ItemPlanoClassificacao.Id);
            model.entidade.Documento = _mapper.Map<DocumentoEntidade>(documento);
            try
            {
                await _core.UpdateAsync(_mapper.Map<TemporalidadeModel>(entidade));

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

        public async Task<TemporalidadeViewModel> Create(TemporalidadeEntidade entidade)
        {
            var model = new TemporalidadeViewModel();
            try
            {
                var modelInsert = await _core.InsertAsync(_mapper.Map<TemporalidadeModel>(entidade));
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

        private ICollection<EnumModel> obterListaUnidadesTempo()
        {
            return new List<EnumModel> {
                    new EnumModel { Id = (int)UnidadeTempoModel.Anos, Nome = UnidadeTempoModel.Anos.ToString() },
                    new EnumModel { Id = (int)UnidadeTempoModel.Dias, Nome = UnidadeTempoModel.Dias.ToString()  },
                    new EnumModel { Id = (int)UnidadeTempoModel.Meses, Nome = UnidadeTempoModel.Meses.ToString()  },
                    new EnumModel { Id = (int)UnidadeTempoModel.Semanas, Nome = UnidadeTempoModel.Semanas.ToString()  },
                };
        }

        private ICollection<EnumModel> obterListaDestinacao()
        {
            return new List<EnumModel> {
                    new EnumModel { Id = (int)DestinacaoFinal.Eliminacao, Nome = "Eliminação" },
                    new EnumModel { Id = (int)DestinacaoFinal.GuardaPermanente, Nome = "Guarda Permanente" },
                };
        }

        public async Task<TemporalidadeViewModel> New(int idDocumento)
        {
            var model = new TemporalidadeViewModel();
            try
            {
                model.Action = "Create";
                model.entidade = new TemporalidadeEntidade();
                model.unidadesTempo = obterListaUnidadesTempo();
                model.destinacoes = obterListaDestinacao();
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
