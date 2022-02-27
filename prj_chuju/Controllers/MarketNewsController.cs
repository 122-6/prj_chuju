using prj_chuju.Models;
using prj_chuju.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class MarketNewsController : Controller
    {
        // GET: MarketNews
        dbchujuEntities1 db = new dbchujuEntities1();
        public ActionResult Index()
        {
            
            var article = from p in db.articleOutline join x in db.articleClass on p.articleClass equals x.id where x.mainClass == "房市新訊" select p;

            return View(article);
        }

        public ActionResult ArticleContent(int id)
        {
            ArticleContentViewModel vm = new ArticleContentViewModel();
            vm.articleContent = db.articleContent.Where(t => t.articleID == id).ToList();
            int articleClass = db.articleOutline.Where(t => t.id == id).Select(p => p.articleClass).First();
            vm.articleOutline = db.articleOutline.Where(t => t.articleClass == articleClass).ToList();
            vm.articleDetail = db.articleOutline.Where(t => t.id == id).ToList();

            // 瀏覽歷史功能
            AccountInfoMemory userInfo = new AccountInfoHelper(Session, Request).Information;
            new factory_CollectAndViewed().visitArticle(userInfo.theid, (int)id);

            return View(vm);
        }

        public ActionResult FeverCase()
        {
            var first = from p in db.articleOutline where p.articleClass1.mainClass == "房市新訊" && p.articleClass1.className == "發燒個案" select p;
            return View(first);
        }

        public ActionResult HouseMarket()
        {
            var member = from p in db.articleOutline where p.articleClass1.mainClass == "房市新訊" && p.articleClass1.className == "房市脈動" select p;
            return View(member);
        }

        public ActionResult Invest()
        {
            var design = from p in db.articleOutline where p.articleClass1.mainClass == "房市新訊" && p.articleClass1.className == "金融投資" select p;
            return View(design);
        }
    }
}