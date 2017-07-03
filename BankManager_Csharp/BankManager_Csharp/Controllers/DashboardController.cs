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
            var is_authenticated = Session["is_authenticated"];
            if (username == null || is_authenticated == null || (bool) is_authenticated == false)
            {
                return RedirectToAction("Index", "Login");
            }



            Account account = MvcApplication.bankManager.getUserAccount(username.ToString());


            return View("Index", new AccountResponse(account, null));
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult MakeTransaction(int amount, String option)
        {
            Response response = null;
            String username = Session["username"].ToString();
            Account account = MvcApplication.bankManager.getUserAccount(username);

            if (option.Equals("Withdraw") == true)
            {
                response = MvcApplication.bankManager.withdraw(account, amount);
            } else if(option.Equals("Deposit") == true)
            {
                response = MvcApplication.bankManager.deposit(account, amount);
            }


            return View("Index", new AccountResponse(account, response));
        }



    }

}