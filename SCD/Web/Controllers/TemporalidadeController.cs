using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using Prodest.Scd.Web.Controllers.Base;
using Prodest.Scd.Web.Filters;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [MessageFilterAttribute]
    public class Temporalidade : BaseController
    {
        ITemporalidadeService _service;

        public Temporalidade(ITemporalidadeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> List(int idPlanoClassificacao)
        {
            var model = new TemporalidadeViewModel
            {
                entidade = new TemporalidadeEntidade
                {
                    Documento = new DocumentoEntidade
                    {
                        ItemPlanoClassificacao = new ItemPlanoClassificacaoEntidade
                        {
                            PlanoClassificacao = new PlanoClassificacaoEntidade
                            {
                                Id = idPlanoClassificacao
                            }
                        }
                    }
                }
            };
            return PartialView("_List", model);
        }

        public async Task<IActionResult> Delete(int id, int idPlanoClassificacao)
        {
            var model = await _service.Delete(id);
            AddHttpContextMessages(model.Result.Messages);
            if (model.Result.Ok)
            {
                return await List(idPlanoClassificacao);
            }
            else
            {
                //Neste cenário a chamada é a mesma independente do resultado do delete
                return await List(idPlanoClassificacao);
            }
        }

        public async Task<IActionResult> Edit(int id, int idPlanoClassificacao)
        {
            var model = await _service.Edit(id);
            AddHttpContextMessages(model.Result.Messages);
            if (model.Result.Ok)
            {
                return PartialView("_Form", model);
            }
            else
            {
                return await List(idPlanoClassificacao);
            }
        }

        public async Task<IActionResult> New(int IdDocumento)
        {
            var model = await _service.New(IdDocumento);
            return PartialView("_Form", model);
        }

        public async Task<IActionResult> Create(TemporalidadeViewModel model)
        {
            TemporalidadeViewModel modelForm = model.Clone() as TemporalidadeViewModel;
            if (model != null && model.entidade != null)
            {
                model = await _service.Create(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(modelForm.entidade.Documento.ItemPlanoClassificacao.PlanoClassificacao.Id);
                }
            }
            //Se de tudo der errado, volta para o formulário
            return PartialView("_Form", modelForm);
        }

        public async Task<IActionResult> Update(TemporalidadeViewModel model)
        {

            if (model != null && model.entidade != null)
            {
                model = await _service.Update(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(model.entidade.Documento.ItemPlanoClassificacao.PlanoClassificacao.Id);
                }
            }
            return await Edit(model.entidade.Id, model.entidade.Documento.ItemPlanoClassificacao.PlanoClassificacao.Id);
        }

    }
}
