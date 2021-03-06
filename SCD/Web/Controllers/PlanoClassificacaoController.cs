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
    public class PlanoClassificacao : BaseController
    {
        IPlanoClassificacaoService _service;
        //public MensagemViewModel mensagens { get; set; }

        public PlanoClassificacao(IPlanoClassificacaoService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _service.Search(null);
            return View(model);
        }
        public async Task<IActionResult> List()
        {
            var model = await _service.Search(null);
            return PartialView("_List", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.Delete(id);
            AddHttpContextMessages(model.Result.Messages);
            if (model.Result.Ok)
            {
                return await List();
            }
            else
            {
                //Neste cenário a chamada é a mesma independente do resultado do delete
                return await List();
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
                return await List();
            }
        }

        public IActionResult New()
        {
            var model = _service.New();
            return PartialView("_Form", model);
        }

        public async Task<IActionResult> Create(PlanoClassificacaoViewModel model)
        {
            var modelForm = model;
            if (model != null && model.entidade != null)
            {
                model = await _service.Create(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List();
                }
            }
            //Se de tudo der errado, volta para o formulário
            return PartialView("_Form", modelForm);
        }

        public async Task<IActionResult> Update(PlanoClassificacaoViewModel model)
        {
            if (model != null && model.entidade != null)
            {
                model = await _service.Update(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List();
                }
            }
            return await Edit(model.entidade.Id);
        }
        #region Fim Vigência
        public async Task<IActionResult> UpdateVigencia(PlanoClassificacaoViewModel model)
        {
            if (model != null && model.entidade != null)
            {
                model = await _service.UpdateVigencia(model.entidade);
                AddHttpContextMessages(model.Result.Messages);
                if (model.Result.Ok)
                {
                    return await List();
                }
            }
            return await Edit(model.entidade.Id);
        }
        public async Task<IActionResult> EncerrarVigencia(int id)
        {
            var model = await _service.EncerrarVigencia(id);
            AddHttpContextMessages(model.Result.Messages);
            if (model.Result.Ok)
            {
                return PartialView("_Form", model);
            }
            else
            {
                return await List();
            }
        }
        #endregion
    }
}
