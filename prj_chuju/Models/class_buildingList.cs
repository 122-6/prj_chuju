using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prj_chuju.Models
{
    public class class_buildingList
        {
        public int id { get; set; }
        public string country { get; set; }
        public string district { get; set; }
        public string buildname { get; set; }
        public string buildcompany { get; set; }
        public string insidespace { get; set; }
        public string floor { get; set; }
        public int space { get; set; }
        public string location { get; set; }
        public string reception { get; set; }
        public string phone { get; set; }
        public string features { get; set; }
        public string situation { get; set; }
        public string pictrueurl { get; set; }
    }

    //public class class_buildingList
    //{
    //    public int id;
    //    public string country;
    //    public string district;
    //    public string buildname;
    //    public string buildcompany;
    //    public string insidespace;
    //    public string floor;
    //    public int space;
    //    public string location;
    //    public string reception;
    //    public string phone;
    //    public string features;
    //    public string situation;
    //    public string pictrueurl;



    //    public class_buildingList(SqlDataReader reader)
    //    {
    //        id = Convert.ToInt32(reader["建案序號"]);
    //        country = reader["縣市"].ToString();
    //        district = reader["地區"].ToString();
    //        buildname = reader["建案名稱"].ToString();
    //        insidespace = reader["坪數規劃"].ToString();
    //        floor = reader["樓層規劃"].ToString();
    //        space = Convert.ToInt32(reader["基地面積"]);
    //        location = reader["基地位置"].ToString();
    //        reception = reader["接待中心"].ToString();
    //        phone = reader["諮詢專線"].ToString();
    //        features = reader["特點"].ToString();
    //        situation = reader["現況"].ToString();
    //        pictrueurl = reader["picURL"].ToString();
    //    }
}