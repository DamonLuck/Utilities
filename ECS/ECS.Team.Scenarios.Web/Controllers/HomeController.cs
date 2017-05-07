using ECS.Team.Scenarios.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECS.Team.Scenarios.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Leagues()
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
            return RedirectToAction(nameof(Fixtures), "Home", new { leagueId = leagueId, roundId = roundId -1 });
        }

        public ActionResult NextRound(long leagueId, long roundId)
        {
            return RedirectToAction(nameof(Fixtures), "Home", new { leagueId = leagueId, roundId = roundId +1});
        }

        public ActionResult NextTurn(long leagueId, long roundId)
        {
            Game.NextTurn();
            return RedirectToAction(nameof(Fixtures), "Home", new { leagueId = leagueId, roundId = roundId});
        }

        public ActionResult SetCaptain(long teamId, long playerId)
        {
            Game.Context.Teams.SetTeamCaptain(teamId, playerId);
            return RedirectToAction(nameof(Players), "Home", new { teamId = teamId });
        }
    }
}