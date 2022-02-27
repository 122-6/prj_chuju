using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace prj_chuju.ViewModels
{
    public class buildingContentInfo
    {
        public int id;
        public string city;
        public string region;
        public string caseName;
        public string company;
        public string area;
        public string floor;
        public string baseArea;
        public string baseLocation;
        public string visitLocation;
        public string phone;
        public string tags;
        public string status;
        public string outterPicURL;
        public List<string> rooms;
        public List<string> content;
        public string titlePicURL;
        public List<string> innerPicURL;

        public buildingContentInfo()
        {
            id = -1;
            city = "";
            region = "";
            caseName = "";
            company = "";
            area = "";
            floor = "";
            baseArea = "";
            baseLocation = "";
            visitLocation = "";
            phone = "";
            tags = "";
            status = "";
            outterPicURL = "";
            titlePicURL = "";
            rooms = new List<string>();
            content = new List<string>();
            innerPicURL = new List<string>();
        }
        public buildingContentInfo(SqlDataReader reader)
        {
            id = Convert.ToInt32(reader["建案序號"]);
            city = reader["縣市"].ToString();
            region = reader["地區"].ToString();
            caseName = reader["建案名稱"].ToString();
            company = reader["營造公司"].ToString();
            area = reader["坪數規劃"].ToString();
            floor = reader["樓層規劃"].ToString();
            baseArea = reader["基地面積"].ToString();
            baseLocation = reader["基地位置"].ToString();
            visitLocation = reader["接待中心"].ToString();
            phone = reader["諮詢專線"].ToString();
            tags = reader["特點"].ToString();
            status = reader["現況"].ToString();
            outterPicURL = reader["picURL"].ToString().Replace('\\','/');
            titlePicURL = reader["pic1"].ToString().Replace('\\', '/');
            rooms = new List<string>();
            content = getContent(reader);
            innerPicURL = getInnerPicURL(reader);
        }
        private List<string> getContent(SqlDataReader reader)
        {
            List<string> result = new List<string>();
            string[] contentCol = new string[] {"文宣1", "文宣2", "文宣3", "文宣4", "文宣5", "文宣6", "文宣7", "文宣8", "文宣9" };
            for(int i = 0; i < 9; i++)
            {
                if (reader[contentCol[i]].Equals(DBNull.Value))
                    break;
                result.Add(reader[contentCol[i]].ToString());
            }
            return result;
        }
        private List<string> getInnerPicURL(SqlDataReader reader)
        {
            List<string> result = new List<string>();
            string[] contentCol = new string[] { "pic2", "pic3", "pic4", "pic5", "pic6", "pic7", "pic8", "pic9", "pic10", };
            for (int i = 0; i < 9; i++)
            {
                if (reader[contentCol[i]].Equals(DBNull.Value))
                    break;
                result.Add(reader[contentCol[i]].ToString().Replace('\\','/'));
            }
            return result;
        }
    }

    public class buildingContentHelper
    {
        private const string dbConnectioniStr = @"" +
            @"data source=chujudbserver.database.windows.net;" +
            @"initial catalog=dbchuju;" +
            @"user id=chujuas;" +
            @"password=P@ssw0rd-chuju;" +
            @"MultipleActiveResultSets=True;" +
            @"Asynchronous Processing=True;" +
            @"";
        public buildingContentInfo getBuildingContentInfoByID(int theid)
        {
            buildingContentInfo output = new buildingContentInfo();

            string sqlstr = "select * from buildingdb as b " +
                "join buildingdetail as d on d.建案序號=b.建案序號 " +
                "where b.建案序號 = @idPara";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@idPara",theid);

            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                output = new buildingContentInfo(reader);
            }
            con.Close();
            return output;
        }

        public bool hasData(int theid)
        {
            bool result = false;
            string sqlstr = "select 建案序號 from buildingdetail where 建案序號 = @idPara";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@idPara", theid);

            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = true;
            }
            con.Close();
            return result;
        }
    }

}