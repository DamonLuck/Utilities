using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class TeamComponentBuilder : IComponentBuilder<TeamComponent>
    {
        public TeamComponent Build()
        {
            return new TeamComponent() { Name = Faker.Name.FullName() };
        }
    }
}
