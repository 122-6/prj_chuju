using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace prj_chuju.Models
{
    public class class_buildingCase
    {
        public int id;
        public string country;
        public string district;
        public string buildname;
        public string buildcompany;
        public string insidespace;
        public string floor;
        public int space;
        public string location;
        public string reception;
        public string phone;
        public string features;
        public string situation;
        public string pictrueurl;
        //public string text1;
        //public string text2;
        //public string text3;
        //public string text4;
        //public string text5;
        //public string text6;
        //public string text7;
        //public string text8;
        //public string text9;
        //public string pic1;
        //public string pic2;
        //public string pic3;
        //public string pic4;
        //public string pic5;
        //public string pic6;
        //public string pic7;
        //public string pic8;
        //public string pic9;
        

        public class_buildingCase (SqlDataReader reader)
        {
            id = Convert.ToInt32(reader["建案序號"]);
            country = reader["縣市"].ToString();
            district = reader["地區"].ToString();
            buildname = reader["建案名稱"].ToString();
            insidespace = reader["坪數規劃"].ToString();
            floor = reader["樓層規劃"].ToString();
            space = Convert.ToInt32(reader["基地面積"]);
            location = reader["基地位置"].ToString();
            reception = reader["接待中心"].ToString();
            phone = reader["諮詢專線"].ToString();
            features = reader["特點"].ToString();
            situation = reader["現況"].ToString();
            pictrueurl = reader["picURL"].ToString();
            //id = Convert.ToInt32(reader["建案序號"]);
            //country = reader["縣市"].ToString();
            //district = reader["地區"].ToString();
            //buildname = reader["建案名稱"].ToString();
            //text1 = reader["文宣1"].ToString();
            //text2 = reader["文宣2"].ToString();
            //text3 = reader["文宣3"].ToString();
            //text4 = reader["文宣4"].ToString();
            //text5 = reader["文宣5"].ToString();
            //text6 = reader["文宣6"].ToString();
            //text7 = reader["文宣7"].ToString();
            //text8 = reader["文宣8"].ToString();
            //text9 = reader["文宣9"].ToString();
            //pic1 = reader["pic1"].ToString();
            //pic2 = reader["pic2"].ToString();
            //pic3 = reader["pic3"].ToString();
            //pic4 = reader["pic4"].ToString();
            //pic5 = reader["pic5"].ToString();
            //pic6 = reader["pic6"].ToString();
            //pic7 = reader["pic7"].ToString();
            //pic8 = reader["pic8"].ToString();
            //pic9 = reader["pic9"].ToString();
        }
    }
}