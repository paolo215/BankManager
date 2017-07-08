using BankManager_Csharp.Models;
using Newtonsoft.Json;
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

        

        [HttpGet]
        public String getAccountInfoAPI(String username)
        {
            if(Session["is_authenticated"] != null && bool.Parse(Session["is_authenticated"].ToString()) == true && Session["username"].Equals(username))
            {
                Account account = MvcApplication.bankManager.getUserAccount(username);
                Response response = new Response(true, "Success!");
                return JsonConvert.SerializeObject(new AccountResponse(account, response));
            }

            return JsonConvert.SerializeObject(new AccountResponse(null, new Response(false, "Fail!")));
        }
 

        [HttpPost]
        public String makeTransactionAPI(String username, int amount, String option)
        {
            Response response = null;
            Account account = MvcApplication.bankManager.getUserAccount(username);
            if (option.Equals("Withdraw") == true)
            {
                response = MvcApplication.bankManager.withdraw(account, amount);
            }
            else if (option.Equals("Deposit") == true)
            {
                response = MvcApplication.bankManager.deposit(account, amount);
            }

            return JsonConvert.SerializeObject(new AccountResponse(account, response));
        }


        [HttpPost]
        public ActionResult MakeTransaction(int amount, String option)
        {
            String username = Session["username"].ToString();
            AccountResponse accountResponse = JsonConvert.DeserializeObject<AccountResponse>(makeTransactionAPI(username, amount, option));

            return View("Index", accountResponse);
        }



    }

}