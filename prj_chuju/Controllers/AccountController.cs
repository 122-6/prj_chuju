﻿using System;
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
        // 各頁面
        public ActionResult Index()
        {
            // 刷新Cookie
            HttpCookie x = new HttpCookie("userInfo");
            if (x != null)
            {
                x.Expires = DateTime.Now.AddDays(14);
                Response.Cookies.Add(x);
            }

            // 點擊上方會員頁面，以AccountInfoHelper確認登入狀態
            int theid = -1;
            AccountInfoHelper aih = new AccountInfoHelper(Session, Request);
            theid = aih.getID();

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
            // 若session登入狀態錯誤或cookie判定非記憶則跳回登入頁面
            AccountInfoHelper aih = new AccountInfoHelper(Session, Request);
            AccountInfoMemory loginInfo = aih.Information;

            int theid = new factory_accountInfo().varifyPassByID(loginInfo.theid, loginInfo.password);
            if (theid <= 0) return RedirectToAction("loginPage");

            // 確認登入與記憶狀況後蒐集資料庫資料以呈現頁面
            class_accountInfo x = new factory_accountInfo().selectAccountByID(theid);
            ViewBag.remember = loginInfo.remember;
            ViewBag.theid = loginInfo.theid;
            ViewBag.password = loginInfo.password;

            AccountInfoPageViewModel aipvm = new AccountInfoPageViewModel(theid);


            //ViewBag.userName = x.userName;
            //ViewBag.gender = x.gender;
            //ViewBag.email = x.email;
            //ViewBag.cellphone = x.cellphone;
            return View(aipvm);
        }
        public ActionResult passToInfoPage()
        {
            // 基本傳入資訊
            string theid = Request["theid"];
            string password = Request["password"];
            string remember = Request["remember"];
            string InfoMessage = $"{remember}|{theid}|{password}";

            // 長期記憶由Cookie實現，短期由Session實現
            HttpCookie x = new HttpCookie("userInfo");
            x.Value = remember == "yes" ? InfoMessage : "";
            x.Expires = DateTime.Now.AddDays(14);
            Response.Cookies.Add(x);

            Session["userInfo"] = remember == "no" ? InfoMessage : "";

            return RedirectToAction("infoPage");
        }

        // 註冊系統
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

            int theid = f.occupiedEmailID(Request["userEmail"]);
            Session["userInfo"] = $"no|{theid}|{Request["password"]}";

            return RedirectToAction("infoPage", "Account");
        }

        // 登入系統
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

        // 登出系統
        public ActionResult logout()
        {
            // 清除Cookie
            HttpCookie x = new HttpCookie("userInfo");
            x.Value = "";
            x.Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies.Add(x);

            // 清除Session
            Session["userInfo"] = "";

            return RedirectToAction("Index","Home");
        }

    }
}