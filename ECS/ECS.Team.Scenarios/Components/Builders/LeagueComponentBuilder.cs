using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class LeagueComponentBuilder : IComponentBuilder
    {
        public IComponent Build()
        {
            return new LeagueComponent() { Name = Faker.Company.Name()};
        }
    }
}
