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
    }
}