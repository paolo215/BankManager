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

        public static BankManager bankManager = new BankManager();

        protected void Application_Start()
        {
            Account account = new Account(1, "admin", "password", "Paolo", "Villanueva", "My Address");
           

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
