using DL.ECS.Core;
using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class LeagueComponentBuilder : BaseComponentBuilder
    {
        public LeagueComponentBuilder(ComponentId componentId)
            : base(componentId)
        {
        }

        public override IComponent Build()
        {
            return new LeagueComponent() { Name = Faker.Company.Name()};
        }
    }
}
