using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class LeagueMembershipComponent : IComponent
    {
        public LeagueMembershipComponent(long leagueId, bool isLeague)
        {
            LeagueId = leagueId;
            IsLeague = isLeague;
        }

        public long LeagueId { get;}
        public bool IsLeague { get; }
    }
}
