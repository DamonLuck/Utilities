using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class LeagueMembershipComponent : IComponent
    {
        public LeagueMembershipComponent(long leagueId)
        {
            LeagueId = leagueId;
        }

        public LeagueMembershipComponent(long leagueId,
            int won, int lost, int draw, int gf, int ga)
        {
            LeagueId = leagueId;
            Won = won;
            Lost = lost;
            Draw = draw;
            Gf = gf;
            Ga = ga;
        }

        public long LeagueId { get;}
        public int Won { get; }
        public int Lost { get; }
        public int Draw { get; }
        public int Gf { get; }
        public int Ga { get; }
    }
}
