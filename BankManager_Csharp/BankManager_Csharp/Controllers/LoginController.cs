using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManager_Csharp.Models;
using Newtonsoft.Json;

namespace BankManager_Csharp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if(Session["username"] != null && Session["is_authenticated"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }


        [HttpPost]
        public String authorizeAPI(String username, String password)
        {
            Response response = MvcApplication.bankManager.authenticateUser(username, password);

            if(response.isSuccessful == false)
            {
                Session["username"] = username;
                Session["is_authenticated"] = true;
            }


            return JsonConvert.SerializeObject(response);

            
        }


        [HttpPost]
        public ActionResult Authorize(String username, String password)
        {
            String error = "";
            try
            {
                Response response = JsonConvert.DeserializeObject<Response>(authorizeAPI(username, password));
                System.Diagnostics.Debug.WriteLine(response.message);
                if (response.isSuccessful == false)
                {
                    return View("Index", response);
                }
                return RedirectToAction("Index", "Dashboard");
            }

            catch (Exception e)
            {
                error = e.ToString();
            }

            return View("Index", new Response(false, error));
        }

    }

   
}