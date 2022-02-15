using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prj_chuju.ViewModels
{
    public class accountCookie
    {
        public string remember;
        public string theid;
        public string password;
        public accountCookie(string cookieValue)
        {
            if (cookieValue.IndexOf('|') == -1)
            {
                remember = "no";
                theid = "-1";
                password = "";
            }
            else
            {
                int FirstBar = cookieValue.IndexOf('|', 0);
                int SecondBar = cookieValue.IndexOf('|', FirstBar + 1);

                remember = cookieValue.Substring(0, FirstBar);
                theid = cookieValue.Substring(FirstBar + 1, SecondBar - FirstBar - 1);
                password = cookieValue.Substring(SecondBar + 1);
            }

        }
    }
}