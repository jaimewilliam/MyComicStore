using MyComicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyComicStore.Controllers
{
    public class LoginController : Controller
    {
        ComicStoreEntities storeDB = new ComicStoreEntities();

        // GET: Login
        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Logged(string Email, string Password)
        {
            string errorMessage = "";

            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var login = storeDB.Registrations.Where(e => e.Email == Email).Where(p => p.Password == Password).FirstOrDefault();
                if(login != null)
                {
                    HttpCookie mycookie = new HttpCookie("login:cookie");
                    mycookie["RegId"] = login.RegId.ToString();
                    Response.Cookies.Add(mycookie);

                    if (login.TypeOfUserId == 3)
                    {
                        errorMessage = "Member";
                    }
                    else
                    {
                        errorMessage = "Admin/Employee";
                    }
                }
                else
                {
                    errorMessage = "Username or Password is invalid!";
                }
            }
            else
            {
                errorMessage = "Enter Email & Password or Register!";
            }

            //string errorMessage = "Username or Password is invalid";
            return this.Content(errorMessage);
        }
    }
}