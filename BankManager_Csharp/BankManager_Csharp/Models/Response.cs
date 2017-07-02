using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public class Response
    {
        public bool isSuccessful { get; }
        public String message { get; }

        public Response(bool isSuccessful, String message)
        {
            this.isSuccessful = isSuccessful;
            this.message = message;
        }

    }
}