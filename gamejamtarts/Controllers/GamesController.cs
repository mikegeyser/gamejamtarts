using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class GamesController : Controller
    {
        private Db db = new Db();

        public ActionResult Index()
        {
            return View(db.AllGames.OrderByDescending(x => x.CreationDate).ToList());
        }

        public ActionResult Details(string code)
        {
            var game = db.AllGames.FirstOrDefault(x => x.Code == code);

            if (game == null)
                return this.RedirectToAction("Index");

            return View(game);
        }

        public ActionResult Reload()
        {
            Game.ResetGames();
            Game.InitialiseGames();
            return null;
        }

    }
}