using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace prj_chuju.Models
{
    public class class_articleOutline
    {
        public int id;
        public int articleClass;
        public DateTime publishDate;
        public string title;
        public string intro;
        public string articleSource;
        public string pictureURL;

        public class_articleOutline() { }
        public class_articleOutline(SqlDataReader reader)
        {
            id = Convert.ToInt32(reader["id"]);
            articleClass = Convert.ToInt32(reader["articleClass"]);
            publishDate = Convert.ToDateTime(reader["publishDate"]);
            title = reader["title"].ToString();
            intro = reader["intro"].ToString();
            articleSource = reader["articleSource"].ToString();
            pictureURL = reader["pictureURL"].ToString();
        }
    }
}