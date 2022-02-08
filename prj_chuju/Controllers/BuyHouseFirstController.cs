using prj_chuju.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class BuyHouseFirstController : Controller
    {
        // GET: BuyHouseFirst
        dbProjectEntities db = new dbProjectEntities();
        public ActionResult AllArticle()
        {
            
            var article = from p in db.articleOutline join x in db.articleClass on p.articleClass equals x.id where x.mainClass == "買房第一站" select p;

            return View(article);
        }

        public ActionResult ArticleContent(int id)
        {
            ArticleContentViewModel vm = new ArticleContentViewModel();
            vm.articleContent = db.articleContent.Where(t => t.articleID == id).ToList();
            int articleClass = db.articleOutline.Where(t => t.id == id).Select(p => p.articleClass).First();
            vm.articleOutline = db.articleOutline.Where(t => t.articleClass == articleClass).ToList();
            vm.articleDetail = db.articleOutline.Where(t => t.id == id).ToList();


            return View(vm);
        }

        public ActionResult FirstStation()
        {
            var first = db.articleOutline.Join(db.articleClass, p => p.articleClass, x => x.id, (p, x) => p).Where(h => h.articleClass1.className == "買房第一站");

            return View(first);
        }

        public ActionResult MemberBenefits()
        {
            var member = from p in db.articleOutline where p.articleClass1.mainClass == "買房第一站" && p.articleClass1.className == "會員好康" select p;
            return View(member);
        }

        public ActionResult Design()
        {
            var design = from p in db.articleOutline where p.articleClass1.mainClass == "買房第一站" && p.articleClass1.className == "設計裝修" select p;
            return View(design);
        }
    }
}