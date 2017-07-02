using BankManager_Csharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankManager_Csharp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var username = Session["username"];
            if (username == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Account account = MvcApplication.accountData[username.ToString()];
            

            return View("Index", account);
        }

        [HttpPost]
        public ActionResult MakeTransaction(int amount, String option)
        {
            System.Diagnostics.Debug.WriteLine(amount);
            System.Diagnostics.Debug.WriteLine(option);
            return View();
        }
    }

}