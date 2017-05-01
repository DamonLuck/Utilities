using DL.ECS.Core;
using DL.ECS.Core.Components;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios.Components
{
    public class ComponentFactory
    {
        private ComponentIds ComponentIds { get; }

        public ComponentFactory()
        {
            ComponentIds = new ComponentIds();
            ComponentLookup = new Dictionary<Type, ComponentId>();
            ComponentLookup.Add(typeof(PlayerComponent), ComponentIds.PlayerComponentId);
            ComponentLookup.Add(typeof(PlayerCaptainComponent), ComponentIds.PlayerCaptainComponentId);
            ComponentLookup.Add(typeof(TeamComponent), ComponentIds.TeamComponentId);
            ComponentLookup.Add(typeof(LeagueComponent), ComponentIds.LeagueComponentId);
        }

        public IDictionary<Type, ComponentId> ComponentLookup;

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
