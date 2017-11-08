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
    public class Sigilo : BaseController
    {
        ISigiloService _service;

        public Sigilo(ISigiloService service)
        {
            _service = service;
        }

        public async Task<IActionResult> List(int idPlanoClassificacao)
        {
            var model = new SigiloViewModel {
                entidade = new SigiloEntidade
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

        public async Task<IActionResult> Create(SigiloViewModel model)
        {
            SigiloViewModel modelForm = model.Clone() as SigiloViewModel;
            if (model != null && model.entidade != null)
            {
                model.entidade.UnidadePrazoTermino = Prodest.Scd.Business.Model.SigiloModel.UnidadePrazoTerminoSigilo.Dias;
                model.entidade.PrazoTermino = 50;
                model.entidade.Grau = Prodest.Scd.Business.Model.SigiloModel.GrauSigilo.InformacaoPessoal;
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

        public async Task<IActionResult> Update(SigiloViewModel model)
        {

            if (model != null && model.entidade != null)
            {
                model.entidade.UnidadePrazoTermino = Prodest.Scd.Business.Model.SigiloModel.UnidadePrazoTerminoSigilo.Dias;
                model.entidade.PrazoTermino = 50;
                model.entidade.Grau = Prodest.Scd.Business.Model.SigiloModel.GrauSigilo.InformacaoPessoal;
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
