using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// sql相關
using System.Data.SqlClient;

namespace prj_chuju.Models
{
    public class class_accountInfo
    {
        public int id;
        public string accountName;
        public string userPassword;
        public string email;
        public string userName;
        public DateTime birthday;
        public string gender;
        public string cellphone;
        public int region;
        public int avatar;

        class_accountInfo(SqlDataReader reader)
        {
            id = Convert.ToInt32(reader["id"]);
            accountName = reader["accountName"].ToString();
            userPassword = reader["userPassword"].ToString();
            email = reader["email"].ToString();
            userName = reader["userName"].ToString();
            birthday = Convert.ToDateTime(reader["birthday"]);
            gender = reader["gender"].ToString();
            cellphone = reader["cellphone"].ToString();
            region = Convert.ToInt32(reader["region"]);
            avatar = Convert.ToInt32(reader["avatar"]);
        }
    }
    public class factory_accountInfo
    {
        private const string dbConnectioniStr = @"" +
            @"data source=chujudbserver.database.windows.net;" +
            @"initial catalog=dbchuju;" +
            @"user id=chujuas;" +
            @"password=P@ssw0rd-chuju;" +
            @"MultipleActiveResultSets=True;" +
            @"Asynchronous Processing=True;" +
            @"";

        public void create(string cellphone, string email)
        {
            string cmdstr = "insert into accountInfo (accountName,userPassword,userName,email,cellphone) " +
                "values (@accountNamePara,@userPasswordPara,@userNamePara,@emailPara,@cellphonePara)";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.Parameters.AddWithValue("@accountNamePara","account");
            cmd.Parameters.AddWithValue("@userPasswordPara","password");
            cmd.Parameters.AddWithValue("@userNamePara","username");
            cmd.Parameters.AddWithValue("@emailPara",email);
            cmd.Parameters.AddWithValue("@cellphonePara",cellphone);

            con.Open();
            int row = cmd.ExecuteNonQuery();
            con.Close();
        }

        public void create(HttpRequestBase req)
        {
            string cmdstr = "insert into accountInfo (accountName,userPassword,userName,email,cellphone,birthday) " +
                "values (@accountNamePara,@userPasswordPara,@userNamePara,@emailPara,@cellphonePara,@birthdyPara)";

            string password = req.Form["password"];
            string userName = req.Form["userName"];
            string userEmail = req.Form["userEmail"];
            string userPhone = req.Form["userPhone"];
            string birthDay = req.Form["birthDay"];

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.Parameters.AddWithValue("@accountNamePara", "account");
            cmd.Parameters.AddWithValue("@userPasswordPara", password);
            cmd.Parameters.AddWithValue("@userNamePara", userName);
            cmd.Parameters.AddWithValue("@emailPara", userEmail);
            cmd.Parameters.AddWithValue("@cellphonePara", userPhone);
            cmd.Parameters.AddWithValue("@birthdyPara", birthDay);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@accountNamePara", "account");
                cmd.Parameters.AddWithValue("@userPasswordPara", password);
                cmd.Parameters.AddWithValue("@userNamePara", userName);
                cmd.Parameters.AddWithValue("@emailPara", userEmail);
                cmd.Parameters.AddWithValue("@cellphonePara", userPhone);
                cmd.Parameters.AddWithValue("@birthdyPara", "1900-1-1");

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {

                }
            }
            
        }

        public bool occupiedEmail(string enterEmail)
        {
            string cmdstr = "select email from accountInfo where email = @emailPara";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@emailPara", enterEmail);
            con.Open();
            reader = cmd.ExecuteReader();
            int row = 0;
            while (reader.Read())
            {
                row++;
            }
            con.Close();
            return row > 0;
        }
        public bool occupiedPhone(string enterPhone)
        {
            string cmdstr = "select cellphone from accountInfo where cellphone = @cellphonePara";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@cellphonePara", enterPhone);
            con.Open();
            reader = cmd.ExecuteReader();
            int row = 0;
            while (reader.Read())
            {
                row++;
            }
            con.Close();
            return row > 0;
        }
    }
}