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


        /// <summary>
        /// Instantiates AccountResponse object. This is mainly used for showing results in /Dashboard/Index view.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="response"></param>
        public AccountResponse(Account account, Response response)
        {
            this.account = account;
            this.response = response;
        }
    }
}