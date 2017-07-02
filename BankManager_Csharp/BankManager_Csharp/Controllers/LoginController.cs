using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManager_Csharp.Models;

namespace BankManager_Csharp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Authorize(String username, String password)
        {

            try
            {
                Response response = AuthenticateUser(username, password);
                System.Diagnostics.Debug.WriteLine(response.message);
                if (response.isSuccessful == false)
                {
                    return View("Index", response);
                }
            }

        
            catch(Exception e)
            {

            }

            return View();
        }


        public Response AuthenticateUser(String username, String password)
        {
            if (MvcApplication.accountData.ContainsKey(username) == false)
            {
                return new Response(false, "Incorrect credentials. Try again.");
            }

            Account account = MvcApplication.accountData[username];

            if(account.getPassword() == password)
            {
                return new Response(true, "Success!");
            }

            return new Response(false, "Incorrect credentials. Try again.");
        }

    }

   
}