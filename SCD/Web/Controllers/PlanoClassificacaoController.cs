using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Presentation.Base;
using Prodest.Scd.Presentation.ViewModel;
using System;

namespace Web.Controllers
{
    public class PlanoClassificacao : Controller
    {
        IPlanoClassificacaoService _service;

        public PlanoClassificacao(IPlanoClassificacaoService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var model = _service.Search();
            return View(model);
        }

        public IActionResult List()
        {
            var model = _service.Search();
            return PartialView("_List", model);
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);

            var model = _service.Search();

            return PartialView("_List", model);
        }

        public IActionResult Read(int id)
        {
            var model = _service.Search(id);

            return PartialView("_Form", model);
        }
        public IActionResult New()
        {
            var model = new PlanoClassificacaoViewModel()
            {
                entidade = new PlanoClassificacaoEntidade()
            };
            return PartialView("_Form", model);
        }

        //[Route("{model}")]
        public IActionResult Create(PlanoClassificacaoViewModel model)
        {

            var modelForm = model;
            try
            {

                if (model != null && model.entidade != null)
                {
                    if (model.entidade.Id == 0)
                    {
                        model.entidade = _service.Create(model.entidade);
                    }
                    model = _service.Search();
                    model.mensagem = "Registro salvo com sucesso! ";
                    return PartialView("_List", model);
                }
            }
            catch (Exception e) {
                modelForm.mensagem = "Não foi possível salvar o registro. " + e.Message;
            }
            return PartialView("_Form", modelForm);
        }

        public IActionResult Update(PlanoClassificacaoViewModel model)
        {
            if (model != null && model.entidade != null)
            {
                if (model.entidade.Id != 0)
                {
                    _service.Update(model.entidade);
                }
            }
            return PartialView("_Form", model.entidade);
        }

    }
}
