using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class DomainContext : Context
    {
        public DomainContext(ComponentFactory componentFactory)
            :base(componentFactory.TotalComponentCount)
        {
            Player = new Player(this, componentFactory);
            Team = new Team(this, componentFactory);
            League = new League(this, componentFactory);
            Competition = new Competition(this, componentFactory);
        }

        public Player Player { get; }

        public Team Team { get; }

        public League League { get; }

        public Competition Competition { get; }
    }
}
