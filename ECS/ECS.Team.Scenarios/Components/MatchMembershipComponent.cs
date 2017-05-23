using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class MatchMembershipComponent : IComponent
    {
        public MatchMembershipComponent(long gameTurn, 
            long leagueId,
            long homeTeamId,
            long awayTeamId,
            int homeGoals,
            int awayGoals,
            bool isGamePlayed = false)
        {
            GameTurn = gameTurn;
            LeagueId = leagueId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
            IsGamePlayed = isGamePlayed;
        }

        public bool IsGamePlayed { get; }
        public long GameTurn { get; }
        public long LeagueId { get; }
        public long HomeTeamId { get; }
        public long AwayTeamId { get; }
        public int HomeGoals { get; }
        public int AwayGoals { get; }
    }
}
