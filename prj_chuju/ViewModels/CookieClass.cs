using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prj_chuju.ViewModels
{
    public class AccountInfoMemory
    {
        public string remember;
        public int theid;
        public string password;
        //public string permission;
        public AccountInfoMemory()
        {
            remember = "no";
            theid = -1;
            password = "";
        }
        public AccountInfoMemory(string cookieValue)
        {
            if (cookieValue.IndexOf('|') == -1)
            {
                remember = "no";
                theid = -1;
                password = "";
            }
            else
            {
                int Bar1 = cookieValue.IndexOf('|', 0);
                int Bar2 = cookieValue.IndexOf('|', Bar1 + 1);

                remember = cookieValue.Substring(0, Bar1);
                theid = Convert.ToInt32(cookieValue.Substring(Bar1 + 1, Bar2 - Bar1 - 1));
                password = cookieValue.Substring(Bar2 + 1);
            }
        }

    }
    public class AccountInfoHelper
    {
        public AccountInfoMemory Information;
        public AccountInfoHelper(HttpSessionStateBase session, HttpRequestBase request)
        {
            Information = new AccountInfoMemory();
            string uif = "";
            if (session["userInfo"] != null)
            {
                string temstr = session["userInfo"].ToString();
                if (temstr != "")
                    uif = temstr;
            }
            if (request.Cookies != null)
            {
                HttpCookie x = request.Cookies["userInfo"];
                string temstr = x != null ? x.Value : "";
                if (temstr != "")
                    uif = temstr;
            }
            Information = new AccountInfoMemory(uif);
        }
        public int getID()
        {
            return Information.theid;
        }
        public void updateCookie(HttpResponseBase response, HttpRequestBase request)
        {
            HttpCookie oldx = request.Cookies["userInfo"];
            HttpCookie newx = new HttpCookie("userInfo");
            newx.Value = oldx != null ? oldx.Value : "";
            newx.Expires = DateTime.Now.AddDays(14);
            response.Cookies.Add(newx);
        }
    }
}