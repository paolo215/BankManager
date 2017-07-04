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

        /// <summary>
        /// Instantiates Respose object. This is used to show meaningful messages to the user.
        /// </summary>
        /// <param name="isSuccessful"></param>
        /// <param name="message"></param>
        public Response(bool isSuccessful, String message)
        {
            this.isSuccessful = isSuccessful;
            this.message = message;
        }

    }
}