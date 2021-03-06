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

// 頭貼相關
using System.IO;

namespace prj_chuju.Controllers
{
    public class AccountController : Controller
    {
        // 各頁面
        public ActionResult Index()
        {
            // 點擊上方會員頁面，以AccountInfoHelper確認登入狀態
            int theid = -1;
            AccountInfoHelper aih = new AccountInfoHelper(Session, Request);
            theid = aih.getID();

            // 刷新Cookie
            aih.updateCookie(Response, Request);

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
            Session["permission"] = x.permission; // 記住登入者的權限

            AccountInfoPageViewModel aipvm = new AccountInfoPageViewModel(theid);

            return View(aipvm);
        }
        public ActionResult passToInfoPage()
        {
            // 基本傳入資訊
            string theid = Request["theid"];
            string password = Request["password"];
            string remember = Request["remember"];
            string InfoMessage = $"{remember}|{theid}|{password}";

            // 長期記憶由Cookie實現:
            HttpCookie x = new HttpCookie("userInfo");
            x.Value = remember == "yes" ? InfoMessage : "";
            x.Expires = DateTime.Now.AddDays(14);
            Response.Cookies.Add(x);
            // 短期記憶由Session實現:
            Session["userInfo"] = remember == "no" ? InfoMessage : "";

            return RedirectToAction("infoPage");
        }
        public ActionResult forgetPassword()
        {
            return View();
        }
        public ActionResult resetPassword(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                factory_accountInfo f = new factory_accountInfo();
                int theid = f.getIDByCode(code);
                f.removeCodeByID(theid);
                ViewBag.theid = theid;
                if(theid<0)
                    return RedirectToAction("Index", "Home");
                else
                    return View();
            }
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
                string subject = $"雛居會員電子郵件驗證通知信";
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
            Session["permission"] = "guest";

            return RedirectToAction("Index","Home");
        }

        // 忘記密碼系統
        public void sendForgetEmail()
        {
            // 變數宣告與資料庫動作
            factory_accountInfo f = new factory_accountInfo();
            string host = Request.Url.Host;
            string userEmail = Request["email"];
            string code = Guid.NewGuid().ToString();
            int theid = f.occupiedEmailID(userEmail);
            f.addForgetCode(theid, code);

            // 寄送連結
            using (SmtpClient theEmail = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("chujumail306@gmail.com", "P@ssw0rd-chuju"),
            })
            {
                string subject = $"雛居會員密碼重設通知信";
                string body = $"親愛的用戶，您好！\n\n" +
                    $"您於 {DateTime.Now} 要求重設密碼，以下為修改密碼的連結:\n\n" +
                    $"http://{host}/Account/resetPassword?code={code}\n\n" +
                    $"為維護您的隱私及保障您的權益，如果您沒有要求變更密碼，請您盡速與我們連繫！\n" +
                    $"謝謝！\n\n";
                try
                {
                    theEmail.Send("chujumail306@gmail.com", userEmail, subject, body);
                }
                catch (SmtpException ex)
                {
                    string msg = ex.StackTrace.ToString();
                }
            }
        }
        public ActionResult resetPasswordWork()
        {
            AccountInfoMemory aim = new AccountInfoHelper(Session, Request).Information;
            string theid = Request["theid"];
            string password = Request["password"];
            string remember = "no";
            string InfoMessage = $"{remember}|{theid}|{password}";

            HttpCookie x = new HttpCookie("userInfo");
            x.Value = remember == "yes" ? InfoMessage : "";
            x.Expires = DateTime.Now.AddDays(14);
            Response.Cookies.Add(x);
            Session["userInfo"] = remember == "no" ? InfoMessage : "";

            new factory_accountInfo().editAccountPassword(Request);

            return RedirectToAction("Index", "Account");
        }

        // 會員頁面相關功能
        public ActionResult updateAccountAvatar(HttpPostedFileBase avatarRound)
        {
            if (avatarRound != null)
            {
                if (avatarRound.ContentLength > 0)
                {
                    // 存檔相關
                    Guid myuuid = Guid.NewGuid();
                    string uuidName = myuuid.ToString();
                    string fileName = uuidName + Path.GetExtension(avatarRound.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/AccountPictures/userAvatar"), fileName);
                    avatarRound.SaveAs(path);

                    // 資料庫相關
                    string avatarStr = "images/AccountPictures/userAvatar/" + fileName;
                    int userID = new AccountInfoHelper(Session,Request).Information.theid;
                    factory_accountInfo f = new factory_accountInfo();
                    f.createAvatar(avatarStr, userID);
                    int avatarID = f.getNewestAvatar(userID);
                    f.updateUserAvatar(userID, avatarID);
                }
            }
            return RedirectToAction("infoPage");
        }
        public void editAccountInfo()
        {
            new factory_accountInfo().editAccountInfo(Request);
        }
        public void editAccountPassword()
        {
            AccountInfoMemory aim = new AccountInfoHelper(Session, Request).Information;
            string theid = Request["theid"];
            string password = Request["password"];
            string remember = aim.remember;
            string InfoMessage = $"{remember}|{theid}|{password}";

            HttpCookie x = new HttpCookie("userInfo");
            x.Value = remember == "yes" ? InfoMessage : "";
            x.Expires = DateTime.Now.AddDays(14);
            Response.Cookies.Add(x);
            Session["userInfo"] = remember == "no" ? InfoMessage : "";

            new factory_accountInfo().editAccountPassword(Request);
        }
        public JsonResult regionObject()
        {
            class_allRegionInfo x = new class_allRegionInfo();
            return Json(x);
        }
        public JsonResult getCollectBuilding()
        {
            int theid = Convert.ToInt32(Request["userID"]);
            factory_userCollectViewed f = new factory_userCollectViewed();
            List<class_userBuildingInfo> x = f.getCollectBuilding(theid);
            return Json(x);
        }
        public JsonResult getViewedBuilding()
        {
            int theid = Convert.ToInt32(Request["userID"]);
            factory_userCollectViewed f = new factory_userCollectViewed();
            List<class_userBuildingInfo> x = f.getViewedBuilding(theid);
            return Json(x);
        }
        public JsonResult getCollectArticle()
        {
            int theid = Convert.ToInt32(Request["userID"]);
            factory_userCollectViewed f = new factory_userCollectViewed();
            List<class_userArticleInfo> x = f.getCollectArticle(theid);
            return Json(x);
        }
        public JsonResult getViewedArticle()
        {
            int theid = Convert.ToInt32(Request["userID"]);
            factory_userCollectViewed f = new factory_userCollectViewed();
            List<class_userArticleInfo> x = f.getViewedArticle(theid);
            return Json(x);
        }
        public JsonResult getBookedCase()
        {
            int theid = Convert.ToInt32(Request["userID"]);
            factory_userCollectViewed f = new factory_userCollectViewed();
            List<class_bookedCaseInfo> x = f.getBookedCase(theid);
            return Json(x);
        }


    }
}