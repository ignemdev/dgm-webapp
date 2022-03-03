using System;
using System.Collections.Generic;
using System.Text;

namespace TeamPlayers.Core.Models
{
    public class ResponseModel<TModel>
    {
        public TModel Result { get; set; } = default!;
        public string ErrorMessage { get; private set; } = default!;
        public bool HasError { get; set; }
        public void SetErrorMessage(string message)
        {
            ErrorMessage = message;
            HasError = true;
        }
    }
}
