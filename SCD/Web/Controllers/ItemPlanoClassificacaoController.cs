﻿using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using Prodest.Scd.Web.Controllers.Base;
using Prodest.Scd.Web.Filters;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [MessageFilterAttribute]
    public class ItemPlanoClassificacao : BaseController
    {
        IItemPlanoClassificacaoService _service;

        public ItemPlanoClassificacao(IItemPlanoClassificacaoService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int id)
        {
            var model = await _service.Search(new FiltroItemPlanoClassificacao { IdPlanoClassificacao = id });
            return View(model);
        }
        public async Task<IActionResult> List(int id)
        {
            var model = await _service.Search(new FiltroItemPlanoClassificacao { IdPlanoClassificacao = id });
            return PartialView("_List", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.Delete(id);
            AddHttpContextMessages(model.Result.Messages);
            if (model.Result.Ok)
            {
                return await List(1);
            }
            else
            {
                //Neste cenário a chamada é a mesma independente do resultado do delete
                return await List(1);
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
                return await List(1);
            }
        }

        public async Task<IActionResult> New()
        {
            var model = await _service.New();
            return PartialView("_Form", model);
        }

        public async Task<IActionResult> Create(ItemPlanoClassificacaoViewModel model)
        {
            var modelForm = model;
            if (model != null && model.entidade != null)
            {
                model = await _service.Create(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(1);
                }
            }
            //Se de tudo der errado, volta para o formulário
            return PartialView("_Form", modelForm);
        }

        public async Task<IActionResult> Update(ItemPlanoClassificacaoViewModel model)
        {
            if (model != null && model.entidade != null)
            {
                model = await _service.Update(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List(1);
                }
            }
            return await Edit(model.entidade.Id);
        }
   
    }
}
