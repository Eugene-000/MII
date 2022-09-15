using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MilitaryAirfield.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string result = "Вы не аутентифицированы";
            if ( Request.Cookies["My localhost cookies"] != null)
            {
                result = "Ваша роль: " + Server.UrlDecode(Request.Cookies["My localhost cookies"]["Role"]);
                ViewBag.Result = result;
            }
            else
            {
                ViewBag.Result = "Вы не аутентифицированы";
            }
            return View();
            
        }
        
        public ActionResult Roles()
        {
                return View();
        }

        public ActionResult LoginOut()
        {
            Response.Cookies["My localhost cookies"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Home");
        }
    }
}