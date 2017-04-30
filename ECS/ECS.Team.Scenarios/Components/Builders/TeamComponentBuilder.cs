using DL.ECS.Core;
using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class TeamComponentBuilder : BaseComponentBuilder
    {
        public TeamComponentBuilder(ComponentId componentId)
            : base(componentId)
        {
        }

        public override IComponent Build()
        {
            return new TeamComponent() { Name = Faker.Name.FullName() };
        }
    }
}
