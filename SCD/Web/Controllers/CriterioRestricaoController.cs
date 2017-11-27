using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using Prodest.Scd.Web.Controllers.Base;
using Prodest.Scd.Web.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [MessageFilterAttribute]
    public class CriterioRestricao : BaseController
    {
        ICriterioRestricaoService _service;

        public CriterioRestricao(ICriterioRestricaoService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int idPlanoClassificacao)
        {
            var model = await _service.Search(new FiltroCriterioRestricao { IdPlanoClassificacao = idPlanoClassificacao });
            return View(model);
        }
        public async Task<IActionResult> List(int idPlanoClassificacao)
        {
            var model = await _service.Search(new FiltroCriterioRestricao { IdPlanoClassificacao = idPlanoClassificacao });
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

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.Edit(id);
            AddHttpContextMessages(model.Result.Messages);
            if (model.Result.Ok)
            {
                return PartialView("_Form", model);
            }
            else
            {
                return await List(model.plano.Id);
            }
        }

        public async Task<IActionResult> New(int idPlanoClassificacao)
        {
            var model = await _service.New(idPlanoClassificacao);
            return PartialView("_Form", model);
        }

        public async Task<IActionResult> Create(CriterioRestricaoViewModel model)
        {
            if (model != null && model.entidade != null)
            {
                model = await _service.Create(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(model.entidade.PlanoClassificacao.Id);
                }
            }
            //Se de tudo der errado, volta para o formulário
            return PartialView("_Form", model);
        }

        public async Task<IActionResult> Update(CriterioRestricaoViewModel model, [FromForm] List<int> Documentos)
        {
            if (model != null && model.entidade != null)
            {
                var docs = new List<DocumentoEntidade>();
                foreach (var item in Documentos)
                {
                    docs.Add(new DocumentoEntidade { Id = item });
                }
                model.entidade.Documentos = docs;
                model = await _service.Update(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(model.entidade.PlanoClassificacao.Id);
                }
            }
            return await Edit(model.entidade.Id);
        }
   
    }
}
