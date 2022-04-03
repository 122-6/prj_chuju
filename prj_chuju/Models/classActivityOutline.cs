using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prj_chuju.Models
{
    public class classActivityOutline
    {
        public int Id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string picture { get; set; }
    }
}