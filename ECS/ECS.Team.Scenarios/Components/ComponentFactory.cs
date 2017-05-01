using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class ComponentFactory
    {
        public int TotalComponentCount { get; }
        public ComponentIds ComponentIds { get; }

        public ComponentFactory()
        {
            TotalComponentCount = 4;
            ComponentIds = new ComponentIds();
        }

        public PlayerComponentBuilder PlayerComponentBuilder()
        {
            return new PlayerComponentBuilder(ComponentIds.PlayerComponentId);
        }

        public PlayerCaptainComponentBuilder PlayerCaptainComponentBuilder()
        {
            return new PlayerCaptainComponentBuilder(ComponentIds.PlayerCaptainComponentId);
        }

        public TeamComponentBuilder TeamComponentBuilder()
        {
            return new TeamComponentBuilder(ComponentIds.TeamComponentId);
        }

        public LeagueComponentBuilder LeagueComponentBuilder()
        {
            return new LeagueComponentBuilder(ComponentIds.LeagueComponentId);
        }
    }
}
