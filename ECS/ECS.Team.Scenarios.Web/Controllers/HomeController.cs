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
    }
}