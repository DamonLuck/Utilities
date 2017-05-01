using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerCaptainComponentBuilder : IComponentBuilder
    {
        public IComponent Build()
        {
            return new PlayerCaptainComponent();
        }
    }
}
