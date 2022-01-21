using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class BrandArticleController : Controller
    {
        // GET: BrandArticle
        public ActionResult Index()
        {
            IEnumerable<articleOutline> list = from p in (new dbtest01Entities()).articleOutline select p;
            return View(list);
        }
    }
}