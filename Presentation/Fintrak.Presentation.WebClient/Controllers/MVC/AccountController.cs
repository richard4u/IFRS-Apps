using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using WebMatrix.WebData;
//using System.Web.Configuration;

namespace Fintrak.Presentation.WebClient.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("account")]
    public class AccountController : ViewControllerBase
    {
        //variable for viewbag message
        //public static string message;

        [ImportingConstructor]
        public AccountController(ISecurityAdapter securityAdapter)
        {

            _SecurityAdapter = securityAdapter;
        }

        ISecurityAdapter _SecurityAdapter;

        //public string _status = WebConfigurationManager.AppSettings["Status"];


        [HttpGet]
        // the route is defined in RouteConfig
        public ActionResult Register()
        {
            _SecurityAdapter.Initialize();

            return View();
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            _SecurityAdapter.Initialize();

            //verify if the user is not authenticated
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //verify if the URL contains  ReturnUrl   
                if (System.Web.HttpContext.Current.Request.Url.ToString().Contains("ReturnUrl"))
                {
                    //redirect the current path
                    System.Web.HttpContext.Current.Response.Redirect("~/Account/Login");
                }

            }

            return View(new AccountLoginModel());

        }

        //[HttpGet]
        //[Route("login")]
        //public ActionResult Login(string returnUrl)
        //{
        //    _SecurityAdapter.Initialize();

        //string configReferrer = ConfigurationManager.AppSettings["expectedreferal"];
        //string defaultReferrer = ConfigurationManager.AppSettings["defaultReferrer"];
        //string actualreferrer = string.Empty;

        //if (HttpContext.Request.UrlReferrer != null)
        //{
        //    actualreferrer = HttpContext.Request.UrlReferrer.ToString();
        //}
        //else
        //{
        //    actualreferrer = "";
        //}

        //if (actualreferrer == "" || actualreferrer == "null")
        //{
        //    actualreferrer = "null";
        //}
        //if (actualreferrer == configReferrer)
        //{
        //    return View(new AccountLoginModel() { ReturnUrl = returnUrl });
        //}
        //else
        //{
        //    //Response.Redirect(defaultReferrer);
        //    //return null;

        //    _SecurityAdapter.Initialize();
        //    //_SecurityAdapter.LogOut();
        //    return RedirectToAction("Login", "Account");
        //}

        //}

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon(); // it clears the session at the end of request.
            System.Web.Security.FormsAuthentication.SignOut(); // NOTE: "System.Web.Security" is used to call FormsAuthentication.SignOut().

            _SecurityAdapter.Initialize();
            //WebSecurity.Logout();
            _SecurityAdapter.LogOut();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Login", "Account");
        }

        //[HttpGet]
        //[Route("logout")]
        //public ActionResult Logout()
        //{
        //    _SecurityAdapter.Initialize();
        //    //WebSecurity.Logout();
        //    _SecurityAdapter.LogOut();
        //    return RedirectToAction("Login", "Account");
        //}

        [HttpGet]
        [Route("changepassword")]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        [Route("forgotpassword")]
        [Authorize]
        public ActionResult ForgotPassword()
        {
            _SecurityAdapter.Initialize();
            return View();
        }


    }
}
