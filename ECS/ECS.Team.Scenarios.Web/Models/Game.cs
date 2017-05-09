using DL.ECS.Team.Scenarios.Domain;
using DL.ECS.Team.Scenarios.Systems;

namespace ECS.Team.Scenarios.Web.Models
{
    public static class Game
    {
        static GameTurnSystem _gameTurnSystem;
        static Game()
        {
            Context = new DomainContext();
            SetupSystem setupSystem =
                new SetupSystem(Context);
            setupSystem.Execute();

            _gameTurnSystem = new GameTurnSystem(Context);
        }
        public static void NextTurn() => _gameTurnSystem.Execute();


        public static DomainContext Context { get; }
    }
}