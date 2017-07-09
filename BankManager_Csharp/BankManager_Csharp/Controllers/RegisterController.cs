using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManager_Csharp.Models;
using Newtonsoft.Json;

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

            return View("Index", null);
        }

        [HttpPost]
        public String createAPI(String username, String password, String firstName,
            String lastName, String address)
        {
            AccountResponse accountResponse = MvcApplication.bankManager.createAccount(username,
                                        password, firstName, lastName, address);


            if(accountResponse.response.isSuccessful == true)
            {
                Session["username"] = username;
                Session["is_authenticated"] = true;
            }

            return JsonConvert.SerializeObject(accountResponse);
        }


        [HttpPost]
        public ActionResult Create(String username, String password, String firstName,
            String lastName, String address)
        {
            String result = createAPI(username, password, firstName, lastName, address);
            AccountResponse accountResponse = JsonConvert.DeserializeObject<AccountResponse>(result);

            
            if(accountResponse.response.isSuccessful == false)
            {
                TempData["username"] = username;
                TempData["firstName"] = firstName;
                TempData["lastName"] = lastName;
                TempData["address"] = address;
                return View("Index", accountResponse.response);
            }

            

            return RedirectToAction("Index", "Dashboard");
        }


    }
}