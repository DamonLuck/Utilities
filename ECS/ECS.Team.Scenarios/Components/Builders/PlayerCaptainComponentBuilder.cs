using DL.ECS.Core;
using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerCaptainComponentBuilder : BaseComponentBuilder
    {
        public PlayerCaptainComponentBuilder(ComponentId componentId)
            :base(componentId)
        {

        }

        public override IComponent Build()
        {
            return new PlayerCaptainComponent();
        }
    }
}
