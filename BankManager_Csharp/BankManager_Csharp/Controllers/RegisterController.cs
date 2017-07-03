using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManager_Csharp.Models;

namespace BankManager_Csharp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            if (Session["username"] != null && Session["is_authenticated"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }



        public ActionResult Create(String username, String password, String firstName,
            String lastName, String address)
        {

            AccountResponse accountResponse = MvcApplication.bankManager.createAccount(username,
                                        password, firstName, lastName, address);

            if(accountResponse.response.isSuccessful == false)
            {
                System.Diagnostics.Debug.WriteLine("fail");
                return View("Index", accountResponse);
            }

            Session["username"] = username;
            Session["is_authenticated"] = true;

            return RedirectToAction("Index", "Dashboard");
        }


    }
}