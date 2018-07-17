using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsuranceClaimRequest.Models;
using System.Web.Security;

namespace InsuranceClaimRequest.Controllers
{
    public class LoginController : Controller
    {

        InsuranceClaimEntites usersEntity = new InsuranceClaimEntites();
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User loginUser)
        {


            if (ModelState.IsValid)
            {
               
                var user = (from userlist in usersEntity.Users
                            where userlist.LoginId == loginUser.LoginId&& userlist.password == loginUser.password
                            select new
                            {
                                userlist.UserId,
                                userlist.First_Name
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {

                    FormsAuthentication.SetAuthCookie(loginUser.LoginId, false);

                    var authTicket = new FormsAuthenticationTicket(1, loginUser.LoginId, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.FirstOrDefault().First_Name);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    //Session["UserName"] = user.FirstOrDefault().First_Name;
                    //Session["UserID"] = user.FirstOrDefault().UserId;
                    return Redirect("/Claims/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
	}
}