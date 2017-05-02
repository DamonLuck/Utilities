using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class LeagueComponentBuilder : IComponentBuilder<LeagueComponent>
    {
        public LeagueComponent Build()
        {
            return new LeagueComponent() { Name = Faker.Company.Name()};
        }
    }
}
