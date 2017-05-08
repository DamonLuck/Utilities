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
        }
        private static MatchSystem _matchSystem;
        public static void NextTurn() => _matchSystem.Execute();

        public static DomainContext Context { get; }
    }
}