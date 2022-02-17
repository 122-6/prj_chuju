﻿using System;
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
                int FirstBar = cookieValue.IndexOf('|', 0);
                int SecondBar = cookieValue.IndexOf('|', FirstBar + 1);

                remember = cookieValue.Substring(0, FirstBar);
                theid = Convert.ToInt32(cookieValue.Substring(FirstBar + 1, SecondBar - FirstBar - 1));
                password = cookieValue.Substring(SecondBar + 1);
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
    }
}