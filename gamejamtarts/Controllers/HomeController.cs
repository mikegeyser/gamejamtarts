using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Driver;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Game.Games().Take(2).ToList());
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
