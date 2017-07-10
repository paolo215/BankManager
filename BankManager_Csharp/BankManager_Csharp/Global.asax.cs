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
        // Instantiates BankManager
        public static BankManager bankManager = new BankManager();
        

        protected void Application_Start()
        {
            BankManagerDBEntities db = new BankManagerDBEntities();
            db.Transactions.Add(new Transaction(1, 1, 1, "a", DateTime.Now));
            db.Accounts.Add(new Account( "a", "a", "a", "a", "a"));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
