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

    public class factory_buildingCase
    {
        private const string dbConnectioniStr = @"" +
            @"data source=chujudbserver.database.windows.net;" +
            @"initial catalog=dbchuju;" +
            @"user id=chujuas;" +
            @"password=P@ssw0rd-chuju;" +
            @"MultipleActiveResultSets=True;" +
            @"Asynchronous Processing=True;" +
            @"";

        string[] theRegion = new string[] { "台北市", "新北市", "桃園市", "新竹市", "新竹縣", "台中市", "台南市", "高雄市", "基隆市", "嘉義市", "彰化縣" };
        string[] theArea = new string[] { "不限", "15坪以下", "15-20坪", "20-30坪", "30-40坪", "40坪以上", "自訂" };
        string[] theRooms = new string[] { "不限", "1", "2", "3", "4", "9" };
        string[] theStatus = new string[] { "不限", "預售屋", "新成屋" };
        string[] theTags = new string[] { "不限", "學區宅", "交通宅", "景觀宅", "制震宅", "防疫宅", "低總價", "低公設", "低首付", "重劃區" };
        int[] theAreaUB = new int[] { 999, 15, 20, 30, 40, 999 };
        int[] theAreaLB = new int[] {   0,  0, 15, 20, 30,  40 };

        public List<class_buildingCase> getBuildingBySelector(HttpRequestBase Request)
        {
            List<class_buildingCase> output = new List<class_buildingCase>();

            string region = Request["region"];
            string rooms = Request["rooms"];
            string status = Request["status"];
            string tags = Request["tags"];
            int areaID = Convert.ToInt32(Request["area"]);
            areaID = areaID >= theAreaUB.Length ? 0 : areaID;
            int areaUB = areaID >= 0 ? theAreaUB[areaID] : 999;
            int areaLB = areaID >= 0 ? theAreaLB[areaID] : 0;

            string regionCond = regionCondition(region);
            string roomsCond = roomsCondition(rooms);
            string statusCond = statusCondition(status);
            string tagsCond = tagsCondition(tags);

            string sqlstr = $"select * from buildingdb where ({regionCond}) and ({roomsCond}) and ({statusCond}) and ({tagsCond})";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader reader;
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                List<int> caseArea = getArea(reader);
                if (caseArea[0] <areaUB && caseArea[1]>areaLB)
                {
                    output.Add(new class_buildingCase(reader));
                }
            }
            con.Close();
            return output;
        }
        private string regionCondition(string region)
        {
            int regionID = Convert.ToInt32(region);
            return regionID >= 0 ? $"縣市='{theRegion[regionID]}' " : "1=1 ";
        }
        private string roomsCondition(string rooms)
        {
            if (rooms == "0") return "1=1 ";
            string output = "";
            for (int i = 1; i < rooms.Length; i++)
            {
                int ind = rooms[i]-'0';
                output += i == 1 ? $"房型規劃 like '%{theRooms[ind]}%' " : $"or 房型規劃 like '%{theRooms[ind]}%' ";
            }
            return output;
        }
        private string statusCondition(string status)
        {
            if (status == "0") return "1=1 ";
            string output = "";
            for (int i = 1; i < status.Length; i++)
            {
                int ind = status[i] - '0';
                output += i == 1 ? $"現況 like '%{theStatus[ind]}%' " : $"or 現況 like '%{theStatus[ind]}%' ";
            }
            return output;
        }
        private string tagsCondition(string tags)
        {
            if (tags == "0") return "1=1 ";
            string output = "";
            for (int i = 1; i < tags.Length; i++)
            {
                int ind = tags[i] - '0';
                output += i == 1 ? $"特點 like '%{theTags[ind]}%' " : $"or 特點 like '%{theTags[ind]}%' ";
            }
            return output;
        }
        private List<int> getArea(SqlDataReader reader)
        {
            List<int> output = new List<int>();
            string str = reader["坪數規劃"].ToString();
            int n = str.Length;
            int i = 0;
            int num = 0;
            while (i < n && '0' <= str[i] && str[i] <= '9')
            {
                num *= 10;
                num += str[i] - '0';
                i++;
            }
            output.Add(num);
            while (i < n && (str[i] < '0' || str[i] > '9'))
            {
                i++;
            }
            if (i < n)
            {
                num = 0;
                while (i < n && '0' <= str[i] && str[i] <= '9')
                {
                    num *= 10;
                    num += str[i] - '0';
                    i++;
                }
            }
            output.Add(num);
            return output;
        }


    }
}