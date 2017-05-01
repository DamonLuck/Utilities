using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class TeamComponentBuilder : IComponentBuilder
    {
        public IComponent Build()
        {
            return new TeamComponent() { Name = Faker.Name.FullName() };
        }
    }
}
