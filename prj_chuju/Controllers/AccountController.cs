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

namespace prj_chuju.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult loginPage()
        {
            return View();
        }

        public ActionResult infoPage()
        {
            return View();
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
            f.create(Request.Form["userPhone"], Request.Form["userEmail"]);
            return RedirectToAction("Index", "Home");
        }
    }
}