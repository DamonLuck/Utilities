using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class MatchMembershipComponent : IComponent
    {
        public MatchMembershipComponent(long gameTurn, 
            long leagueId,
            long homeTeamId,
            long awayTeamId,
            int homeGoals,
            int awayGoals)
        {
            GameTurn = gameTurn;
            LeagueId = leagueId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }

        public long GameTurn { get; }
        public long LeagueId { get; }
        public long HomeTeamId { get; }
        public long AwayTeamId { get; }
        public int HomeGoals { get; }
        public int AwayGoals { get; }
    }
}
