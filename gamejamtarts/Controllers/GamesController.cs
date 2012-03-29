using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult Index()
        {
            return View(Game.SampleGames());
        }

        public ActionResult Details(string code)
        {
            var game = Game.SampleGames().FirstOrDefault(x => x.Code == code);

            if (game == null)
                return this.RedirectToAction("Index");

            return View(game);
        }
    }
}
