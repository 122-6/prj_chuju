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

    
}