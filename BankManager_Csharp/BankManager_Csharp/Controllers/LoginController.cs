using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankManager_Csharp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("a");
            return View();
        }

        [HttpPost]
        public ActionResult Authorize()
        {
            System.Diagnostics.Debug.WriteLine("yay");
         
            return View();
        }

    }

   
}