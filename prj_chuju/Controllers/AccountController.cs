using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// email相關
using System.Net;
using System.Net.Mail;

// sql相關
using System.Data.SqlClient;
using prj_chuju.Models;
using prj_chuju.ViewModels;

namespace prj_chuju.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            HttpCookie x = Request.Cookies["userInfo"];
            if (x==null)
                return RedirectToAction("loginPage");
            accountCookie xc = new accountCookie(x.Value);
            int theid = Convert.ToInt32(xc.theid);
            theid = new factory_accountInfo().varifyPassByID(theid, xc.password);

            if (theid > 0)
                return RedirectToAction("infoPage");
            return RedirectToAction("loginPage");
        }

        public ActionResult loginPage()
        {
            return View();
        }
        public ActionResult basicInfo()
        {
            ViewBag.userEmail = Request.Form["userEmail"];
            ViewBag.userPhone = Request.Form["userPhone"];
            return View();
        }

        public ActionResult infoPage()
        {
            HttpCookie x = Request.Cookies["userInfo"];
            accountCookie xc = new accountCookie(x.Value);
            ViewBag.remember = xc.remember;
            ViewBag.theid = xc.theid;
            ViewBag.password = xc.password;
            return View();
        }
        public ActionResult passToInfoPage()
        {
            string theid = Request["theid"];
            string password = Request["password"];
            string remember = Request["remember"];
            string cookieMessage = $"{remember}|{theid}|{password}";

            HttpCookie x = new HttpCookie("userInfo");
            x.Value = remember == "yes" ? cookieMessage : "";
            x.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(x);

            return RedirectToAction("infoPage");
        }

        [HttpPost]
        public void sendMail()
        {
            string theMail = Request["theMail"];
            string theSecret = Request["theSecret"];

            using (SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("chujumail306@gmail.com", "P@ssw0rd-chuju"),
            })
            {
                string subject = $"雛居帳號驗證碼";
                string body = $"感謝您註冊雛居廣告帳戶！\n\n您的驗證碼為: {theSecret}\n\n於 {DateTime.Now} 發送";
                try
                {
                    email.Send("chujumail306@gmail.com", theMail, subject, body);
                }
                catch (SmtpException ex)
                {
                    string msg = ex.StackTrace.ToString();
                }
            }
        }

        [HttpPost]
        public ActionResult create()
        {
            factory_accountInfo f = new factory_accountInfo();
            f.create(Request);
            return RedirectToAction("infoPage", "Account");
        }
        [HttpPost]
        public bool occupiedEmail()
        {
            string enterEmail = Request["enterEmail"];
            return new factory_accountInfo().occupiedEmailID(enterEmail)>0;
        }
        [HttpPost]
        public bool occupiedPhone()
        {
            string enterPhone = Request["enterPhone"];
            return new factory_accountInfo().occupiedPhoneID(enterPhone)>0;
        }

        [HttpPost]
        public string varifyAccount()
        {
            string loginType = Request["loginType"];
            string userLogInfo = Request["userLogInfo"];

            int theUserID = -1;
            switch (loginType)
            {
                case "email":
                    theUserID = new factory_accountInfo().occupiedEmailID(userLogInfo);
                    break;
                case "phone":
                    theUserID = new factory_accountInfo().occupiedPhoneID(userLogInfo);
                    break;
                default:
                    return "none";
            }

            return theUserID.ToString();
        }
        [HttpPost]
        public string varifyPassword()
        {
            int theid = Convert.ToInt32(Request["theid"]);
            string password = Request["password"];
            int thdUserID = new factory_accountInfo().varifyPassByID(theid, password);
            return thdUserID.ToString();
        }
    }
}