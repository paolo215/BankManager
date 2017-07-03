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



            Account account = MvcApplication.accountData[username.ToString()];
            

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
            Account account = MvcApplication.accountData[username];

            if (option.Equals("Withdraw") == true)
            {
                response = withdraw(account, amount);
            } else if(option.Equals("Deposit") == true)
            {
                response = deposit(account, amount);
            }


            return View("Index", new AccountResponse(account, response));
        }


        public Response withdraw(Account account, int amount)
        {
            int balance = account.balance;

            if(balance >= amount && amount > 0)
            {
                account.withdraw(amount);
                createTransaction(account, amount, "WITHDRAW");

                return new Response(true, "Success! Your balance is now: " + account.balance);
            }

            return new Response(false, "Insufficient amount of founds. Your balance is currently " + account.balance);
        }


        public Response deposit(Account account, int amount)
        {
            int balance = account.balance;

            if(amount > 0)
            {
                account.deposit(amount);
                createTransaction(account, amount, "DEPOSIT");

                return new Response(true, "Success! Your balance is now: " + account.balance);
            }

            return new Response(false, "Invalid amount of funds. Try again.");
        }


        public Transaction createTransaction(Account account, int amount, String status)
        {
            int balance = account.balance;
            int accountId = account.accountId;
            DateTime date = DateTime.Now;

            Transaction transaction = new Transaction(MvcApplication.nextTransactionId,
                accountId, balance, amount, status, date);

            MvcApplication.transactionData[MvcApplication.nextTransactionId]= transaction;
            account.addTransaction(transaction);

            MvcApplication.nextTransactionId += 1;

            return transaction;
        }

    }

}