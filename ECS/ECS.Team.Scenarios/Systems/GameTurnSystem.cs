using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Domain;

namespace DL.ECS.Team.Scenarios.Systems
{
    public class GameTurnSystem : ISystem
    {
        private MatchSystem _matchSystem;
        public static int CurrentTurn = 1;
        private LeagueUpdateSystem _leagueUpdateSystem;
        public GameTurnSystem(DomainContext context)
        {
            _matchSystem = new MatchSystem(context);
            _leagueUpdateSystem = new LeagueUpdateSystem(context);
        }

        public void Execute()
        {
            _matchSystem.Execute();
            _leagueUpdateSystem.Execute();
            CurrentTurn++;
        }
    }
}
