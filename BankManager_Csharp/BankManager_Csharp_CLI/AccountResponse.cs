using System;


namespace BankManager_Csharp_CLI
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