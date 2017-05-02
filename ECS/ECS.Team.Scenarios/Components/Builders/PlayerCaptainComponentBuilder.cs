using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerCaptainComponentBuilder : IComponentBuilder<PlayerCaptainComponent>
    {
        public PlayerCaptainComponent Build()
        {
            return new PlayerCaptainComponent();
        }
    }
}
