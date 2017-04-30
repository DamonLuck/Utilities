using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class ComponentIds
    {
        public ComponentIds()
        {
            PlayerComponentId = new ComponentId(0);
            TeamComponentId = new ComponentId(1);
        }

        public ComponentId PlayerComponentId { get; }

        public ComponentId TeamComponentId { get; }
    }
}
