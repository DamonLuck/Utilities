using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerComponentBuilder: IComponentBuilder
    {
        public IComponent Build()
        {
            return new PlayerComponent() { Name = Faker.Name.FullName() };
        }
    }
}
