using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class DomainContext : Context
    {
        public DomainContext()
            :base(ComponentFactory.ComponentLookup)
        {
            Players = new Players(this);
            Teams = new Teams(this);
            League = new League(this);
            Fixtures = new Fixtures(this);
        }

        public Players Players { get; }

        public Teams Teams { get; }

        public League League { get; }

        public Fixtures Fixtures { get; set; }
    }
}
