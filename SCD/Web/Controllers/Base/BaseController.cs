using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Web.Controllers.Base
{
    public class BaseController : Controller 
    {
        protected void AddHttpContextMessages(List<MessageViewModel> messages)
        {
            var _messages = HttpContext.Items["messages"] as List<MessageViewModel>;
            _messages = _messages ?? new List<MessageViewModel>();
            _messages.AddRange(messages);
            HttpContext.Items["messages"] = _messages;
        }
    }
}
