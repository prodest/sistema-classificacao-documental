using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Presentation.ViewModel.Base
{
    public class BaseViewModel
    {
        public ResultViewModel Result { get; set; }
        public string Action { get; set; }
        
    }
    public class ResultViewModel
    {
        public List<MessageViewModel> Messages { get; set; }
        public bool Ok { get; set; }
    }
    public class MessageViewModel
    {
        public string Message { get; set; }
        public TypeMessageViewModel Type { get; set; }
    }

    public enum TypeMessageViewModel
    {
        Sucess = 1,
        Fail = 2,
        Alert = 3
    }
}
