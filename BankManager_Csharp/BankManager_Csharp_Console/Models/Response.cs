using System;
using System.Collections.Generic;

namespace BankManager_Csharp_Console.Models
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