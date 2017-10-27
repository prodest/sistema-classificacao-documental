using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using Prodest.Scd.Web.Controllers.Base;
using Prodest.Scd.Web.Filters;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [MessageFilterAttribute]
    public class Documento : BaseController
    {
        IDocumentoService _service;

        public Documento(IDocumentoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> List(int idPlanoClassificacao)
        {
            var model = new DocumentoViewModel {
                entidade = new DocumentoEntidade
                {
                    ItemPlanoClassificacao = new ItemPlanoClassificacaoEntidade
                    {
                        PlanoClassificacao = new PlanoClassificacaoEntidade
                        {
                            Id = idPlanoClassificacao
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

        public async Task<IActionResult> New(int IdItemPlanoClassificacao)
        {
            var model = await _service.New(IdItemPlanoClassificacao);
            return PartialView("_Form", model);
        }

        public async Task<IActionResult> Create(DocumentoViewModel model)
        {
            var modelForm = model;
            if (model != null && model.entidade != null)
            {
                model = await _service.Create(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(model.entidade.ItemPlanoClassificacao.PlanoClassificacao.Id);
                }
            }
            //Se de tudo der errado, volta para o formulário
            return PartialView("_Form", modelForm);
        }

        public async Task<IActionResult> Update(DocumentoViewModel model)
        {
            if (model != null && model.entidade != null)
            {
                model = await _service.Update(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(model.entidade.ItemPlanoClassificacao.PlanoClassificacao.Id);
                }
            }
            return await Edit(model.entidade.Id, model.entidade.ItemPlanoClassificacao.PlanoClassificacao.Id);
        }
   
    }
}
