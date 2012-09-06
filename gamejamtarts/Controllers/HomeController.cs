using System;
using System.Linq;
using System.Web.Mvc;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class HomeController : Controller
    {
        private Db db = new Db();

        public ActionResult Index()
        {
            return View(db.AllGames.OrderByDescending(x => x.CreationDate).Take(5).ToList());
        }

        public ActionResult About()
        {
            return View(Question.DefaultQuestions());
        }

        public ActionResult Theme()
        {
            return View();
        }
    }
}
