using System;

namespace BankManager_Csharp_CLI
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