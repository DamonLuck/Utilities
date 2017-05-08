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

        public LeagueMembershipComponent(long leagueId, bool isLeague, 
            int won, int lost, int draw, int gf, int ga)
        {
            LeagueId = leagueId;
            IsLeague = isLeague;
            Won = won;
            Lost = lost;
            Draw = draw;
            GF = gf;
            GA = ga;
        }

        public long LeagueId { get;}
        public bool IsLeague { get; }
        public int Won { get; }
        public int Lost { get; }
        public int Draw { get; }
        public int GF { get; }
        public int GA { get; }
    }
}
