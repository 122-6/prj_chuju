using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prj_chuju.ViewModels
{
    public class HomePageViewModel
    {
        public List<buildingdb> building { get; set; }
        public List<ActivityOutline> ActivityOutline { get; set; }
        public List<ActivityContent> ActivityContent { get; set; }
    }
}