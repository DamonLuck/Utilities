using System.Web.Mvc;
using ECS.Team.Scenarios.Web.Models;

namespace ECS.Team.Scenarios.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Game.Context.League.GetAll());
        }

        public ActionResult Teams(long leagueId)
        {
            return View(Game.Context.Teams.GetAll(leagueId));
        }

        public ActionResult Players(long teamId)
        {
            return View(Game.Context.Players.GetAll(teamId));
        }

        public ActionResult Fixtures(long leagueId, int roundId)
        {
            return View(Game.Context.Fixtures.GetRound(leagueId, roundId));
        }

        public ActionResult PrevRound(long leagueId, long roundId)
        {
            return RedirectToAction(nameof(Fixtures), "Home", new {leagueId, roundId = roundId -1 });
        }

        public ActionResult NextRound(long leagueId, long roundId)
        {
            return RedirectToAction(nameof(Fixtures), "Home", new {leagueId, roundId = roundId +1});
        }

        public ActionResult Simulate(string url)
        {
            Game.NextTurn();
            return Redirect(url);
        }

        public ActionResult NextTurn(long leagueId, long roundId)
        {
            Game.NextTurn();
            return RedirectToAction(nameof(Fixtures), "Home", new {leagueId, roundId});
        }

        public ActionResult SetCaptain(long teamId, long playerId)
        {
            Game.Context.Teams.SetTeamCaptain(teamId, playerId);
            return RedirectToAction(nameof(Players), "Home", new {teamId });
        }
    }
}