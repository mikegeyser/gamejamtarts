using System.Web.Mvc;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View(Question.DefaultQuestions());
        }
    }
}
