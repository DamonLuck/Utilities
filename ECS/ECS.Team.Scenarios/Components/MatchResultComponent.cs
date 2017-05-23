using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class MatchResultComponent : IComponent
    {
        public MatchResultComponent(
            long leagueId,
            long homeTeamId,
            long awayTeamId,
            int homeGoals,
            int awayGoals)
        {
            LeagueId = leagueId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }

        public long LeagueId { get; }
        public long HomeTeamId { get; }
        public long AwayTeamId { get; }
        public int HomeGoals { get; }
        public int AwayGoals { get; }
    }
}
