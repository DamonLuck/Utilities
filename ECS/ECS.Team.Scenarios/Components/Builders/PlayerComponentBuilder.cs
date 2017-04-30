using DL.ECS.Core;
using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerComponentBuilder: BaseComponentBuilder
    {
        public PlayerComponentBuilder(ComponentId componentId)
            :base(componentId)
        {
        }

        public override IComponent Build()
        {
            return new PlayerComponent() { Name = Faker.Name.FullName() };
        }
    }
}
