using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class ComponentIds
    {
        public ComponentIds()
        {
            PlayerComponentId = new ComponentId(0);
            TeamComponentId = new ComponentId(1);
            LeagueComponentId = new ComponentId(2);
            PlayerCaptainComponentId = new ComponentId(3);
        }

        public ComponentId PlayerComponentId { get; }
        public ComponentId PlayerCaptainComponentId { get; }

        public ComponentId TeamComponentId { get; }
        public ComponentId LeagueComponentId { get; }
    }
}
