using MilitaryAirfield.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MilitaryAirfield.Controllers
{
    public class AccountController : Controller
    {
        public static HttpCookie cookies = new HttpCookie("My localhost cookies");
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                
                using (MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4())
                {
                    user = db.User.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                    
                    if (user != null)
                    {
                        Role role = db.Role.Find(user.RolePlace);
                        if (role != null)
                        {
                            string roles = role.RoleName;
                            cookies["Role"] = Server.UrlEncode(roles.ToString());
                            Response.Cookies.Add(cookies);
                            return RedirectToAction("Roles", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким логином не существует");
                    }
                }
                
            }
            return View(model);

        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4())
                {
                    user = db.User.FirstOrDefault(u => u.Login == model.Login);
                }
                if (user == null)
                {
                    using (MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4())
                    {
                        db.User.Add(new User { Login = model.Login, Password = model.Password, RolePlace = model.PlaceOfWork });
                        db.SaveChanges();

                        user = db.User.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                        if (user != null)
                        {
                            Role role = db.Role.Find(user.RolePlace);
                            if (role != null)
                            {
                                string roles = role.RoleName;
                                cookies["Role"] = Server.UrlEncode(roles.ToString());
                                Response.Cookies.Add(cookies);
                                return RedirectToAction("Roles", "Home");
                            }
                        }
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
