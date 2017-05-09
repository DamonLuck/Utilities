using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;
using DL.ECS.Team.Scenarios.Systems;

namespace ECS.Team.Scenarios.Web.Models
{
    public static class Game
    {
        static Game()
        {
            Context = new DomainContext();
            SetupSystem setupSystem =
                new SetupSystem(Context);
            setupSystem.Execute();

            _matchSystem = new MatchSystem(Context);
            _leagueUpdateSystem = new LeagueUpdateSystem(Context);
        }
        private static MatchSystem _matchSystem;
        private static LeagueUpdateSystem _leagueUpdateSystem;
        public static void NextTurn()
        {
            _matchSystem.Execute();
            _leagueUpdateSystem.Execute();
        }


        public static DomainContext Context { get; }
    }
}