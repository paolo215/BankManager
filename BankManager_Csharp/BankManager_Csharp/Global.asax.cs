using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BankManager_Csharp.Models;

namespace BankManager_Csharp
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public static Dictionary<String, Account> accountData = new Dictionary<String, Account>();
        public static Dictionary<int, Transaction> transactionData = new Dictionary<int, Transaction>();
        public static int nextAccountId = 1;
        public static int nextTransactionId = 1;

        protected void Application_Start()
        {
            Account account = new Account(1, "admin", "password", "Paolo", "Villanueva", "My Address");
            Transaction transaction = new Transaction(1, 1, 100, 100, "WITHDRAW", DateTime.Now);
            account.addTransaction(transaction);
            accountData["admin"] = account;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
