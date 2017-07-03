using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public class Dashboard
    {
        public Account account { get; }
        public Response response { get; }

        public Dashboard(Account account, Response response)
        {
            this.account = account;
            this.response = response;
        }
    }
}