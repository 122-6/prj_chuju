using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prj_chuju.ViewModels
{
    public class ArticleContentViewModel
    {
        
        public List<articleOutline> articleOutline { get; set; }
        public List<articleContent> articleContent { get; set; }
        public List<articleOutline> articleDetail { get; set; }
        
    }
}