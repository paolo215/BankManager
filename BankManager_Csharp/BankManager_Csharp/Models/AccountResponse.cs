using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public class AccountResponse
    {
        public Account account { get; }
        public Response response { get; }

        public AccountResponse(Account account, Response response)
        {
            this.account = account;
            this.response = response;
        }
    }
}