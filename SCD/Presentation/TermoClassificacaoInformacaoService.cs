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
using static Prodest.Scd.Business.Model.TermoClassificacaoInformacaoModel;

namespace Prodest.Scd.Presentation
{
    public class TermoClassificacaoInformacaoService : ITermoClassificacaoInformacaoService
    {
        private ITermoClassificacaoInformacaoCore _core;
        private ICriterioRestricaoCore _coreCriterio;
        private IPlanoClassificacaoCore _corePlanoClassificacao;
        private IDocumentoCore _coreDocumento;
        private IMapper _mapper;

        public TermoClassificacaoInformacaoService(ITermoClassificacaoInformacaoCore core, IPlanoClassificacaoCore corePlanoClassificacao, IDocumentoCore coreDocumento, ICriterioRestricaoCore coreCriterio, IMapper mapper)
        {
            _core = core;
            _mapper = mapper;
            _corePlanoClassificacao = corePlanoClassificacao;
            _coreDocumento = coreDocumento;
            _coreCriterio = coreCriterio;
        }

        public async Task<TermoClassificacaoInformacaoViewModel> Delete(int id)
        {
            var model = new TermoClassificacaoInformacaoViewModel();
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

        public async Task<TermoClassificacaoInformacaoViewModel> SearchDocumentosByCriterio(int IdCriterio)
        {
            var model = new TermoClassificacaoInformacaoViewModel();
            try
            {
                if (IdCriterio != 0)
                {
                    model.entidade = new TermoClassificacaoInformacaoEntidade
                    {
                        CriterioRestricao = _mapper.Map<CriterioRestricaoEntidade>(await _coreCriterio.SearchAsync(IdCriterio))
                    };
                }
                else
                {
                    model.entidade = new TermoClassificacaoInformacaoEntidade
                    {
                        CriterioRestricao = new CriterioRestricaoEntidade
                        {
                            Documentos = new List<DocumentoEntidade>()
                        }
                    };
                }
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

        public async Task<TermoClassificacaoInformacaoViewModel> Edit(int id)
        {
            var model = new TermoClassificacaoInformacaoViewModel();
            try
            {
                model.Action = "Update";
                model.entidade = _mapper.Map<TermoClassificacaoInformacaoEntidade>(await _core.SearchAsync(id));
                model.unidadesTempo = obterListaUnidadesTempo();
                model.graus = obterListaGraus();
                model.tiposSigilo = obterListaTiposSigilo();
                model.Criterios = _mapper.Map<List<CriterioRestricaoEntidade>>(await _coreCriterio.SearchByPlanoClassificacaoAsync(model.entidade.CriterioRestricao.PlanoClassificacao.Id));
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

        public async Task<TermoClassificacaoInformacaoViewModel> Update(TermoClassificacaoInformacaoEntidade entidade)
        {
            var model = new TermoClassificacaoInformacaoViewModel();
            model.entidade = entidade;
            try
            {
                await _core.UpdateAsync(_mapper.Map<TermoClassificacaoInformacaoModel>(entidade));
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


        private ICollection<EnumModel> obterListaGraus()
        {
            return new List<EnumModel> {
                    new EnumModel { Id = (int)GrauSigiloModel.Reservado, Nome = "Reservado" },
                    new EnumModel { Id = (int)GrauSigiloModel.Secreto, Nome = "Secreto" },
                    new EnumModel { Id = (int)GrauSigiloModel.Ultrassecreto, Nome = "Ultrassecreto" },
                };
        }

        private ICollection<EnumModel> obterListaTiposSigilo()
        {
            return new List<EnumModel> {
                    new EnumModel { Id = (int)TipoSigiloModel.Parcial, Nome = "Parcial" },
                    new EnumModel { Id = (int)TipoSigiloModel.Total, Nome = "Total" },
                };
        }

        private ICollection<EnumModel> obterListaUnidadesTempo()
        {
            return new List<EnumModel> {
                    new EnumModel { Id = (int)UnidadeTempoModel.Anos, Nome = UnidadeTempoModel.Anos.ToString() },
                    new EnumModel { Id = (int)UnidadeTempoModel.Dias, Nome = UnidadeTempoModel.Dias.ToString()  },
                    new EnumModel { Id = (int)UnidadeTempoModel.Meses, Nome = UnidadeTempoModel.Meses.ToString()  },
                    //new EnumModel { Id = (int)UnidadeTempo.Semanas, Nome = UnidadeTempo.Semanas.ToString()  },
                };
        }


        public async Task<TermoClassificacaoInformacaoViewModel> Create(TermoClassificacaoInformacaoEntidade entidade)
        {
            var model = new TermoClassificacaoInformacaoViewModel();
            try
            {
                await _core.InsertAsync(_mapper.Map<TermoClassificacaoInformacaoModel>(entidade));
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

        public async Task<TermoClassificacaoInformacaoViewModel> Search(FiltroTermoClassificacaoInformacao filtro)
        {
            var model = new TermoClassificacaoInformacaoViewModel();
            model.plano = new PlanoClassificacaoEntidade { Id = filtro.IdPlanoClassificacao };
            var entidades = await _core.SearchByUserAsync();
            model.entidades = _mapper.Map<List<TermoClassificacaoInformacaoEntidade>>(entidades);
            model.Result = new ResultViewModel
            {
                Ok = true
            };
            return model;
        }

        public async Task<TermoClassificacaoInformacaoViewModel> New(int idPlanoClassificacao)
        {
            var model = new TermoClassificacaoInformacaoViewModel();
            try
            {
                model.Action = "Create";
                model.entidade = new TermoClassificacaoInformacaoEntidade
                {
                    CriterioRestricao = new CriterioRestricaoEntidade
                    {
                        PlanoClassificacao = new PlanoClassificacaoEntidade { Id = idPlanoClassificacao },
                        Documentos = new List<DocumentoEntidade>()
                    }
                };
                model.graus = obterListaGraus();
                model.unidadesTempo = obterListaUnidadesTempo();
                model.tiposSigilo = obterListaTiposSigilo();
                model.Criterios = _mapper.Map<List<CriterioRestricaoEntidade>>(await _coreCriterio.SearchByPlanoClassificacaoAsync(idPlanoClassificacao));
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
