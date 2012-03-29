using System.Linq;
using System.Web.Mvc;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Game.SampleGames().Take(3).ToList());
        }

        public ActionResult About()
        {
            return View(Question.DefaultQuestions());
        }
    }
}
