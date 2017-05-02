using System;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios.Components
{
    public class ComponentFactory
    {
        public ComponentFactory()
        {
            ComponentLookup = new List<Type>()
            {
                typeof(PlayerComponent),
                typeof(PlayerCaptainComponent),
                typeof(TeamComponent),
                typeof(LeagueComponent),
                typeof(PersonComponent)
            };
        }

        public IList<Type> ComponentLookup;

        public PlayerComponentBuilder PlayerComponentBuilder()
        {
            return new PlayerComponentBuilder();
        }

        public PlayerCaptainComponentBuilder PlayerCaptainComponentBuilder()
        {
            return new PlayerCaptainComponentBuilder();
        }

        public TeamComponentBuilder TeamComponentBuilder()
        {
            return new TeamComponentBuilder();
        }

        public LeagueComponentBuilder LeagueComponentBuilder()
        {
            return new LeagueComponentBuilder();
        }
    }
}
