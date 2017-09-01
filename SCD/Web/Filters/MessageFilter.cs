using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Web.Filters
{
    public class MessageFilterAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
        //        filterContext.ActionDescriptor.ActionName);

        //    base.OnActionExecuting(filterContext);
        //}

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var messages = filterContext.HttpContext.Items["messages"] as List<MessageViewModel>;
            var result = filterContext.Result as ActionResult;
            var model = result == null ? new BaseViewModel() : (result is PartialViewResult ? ((PartialViewResult)result).Model : ((ViewResult)result).Model) as BaseViewModel;
            model.Result = model.Result ?? new ResultViewModel();
            model.Result.Messages = model.Result.Messages ?? new List<MessageViewModel>();
            if (messages != null)
            {
                model.Result.Messages.AddRange(messages);
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
