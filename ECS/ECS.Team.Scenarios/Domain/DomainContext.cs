using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class DomainContext : Context
    {
        public DomainContext(ComponentFactory componentFactory)
            :base(componentFactory.TotalComponentCount)
        {
            Players = new Players(this, componentFactory);
            Teams = new Teams(this, componentFactory);
            League = new League(this, componentFactory);
            Competition = new Competition(this, componentFactory);
        }

        public Players Players { get; }

        public Teams Teams { get; }

        public League League { get; }

        public Competition Competition { get; }
    }
}
