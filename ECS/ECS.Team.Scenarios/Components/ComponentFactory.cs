using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public static class ComponentFactory
    {
        public static Context CreateContext()
        {
            return new Context(TotalComponentCount);
        }

        public const int PlayerComponentIndex = 0;
        public const int TeamComponentIndex = 1;
        public const int TotalComponentCount = 2;

        public static PlayerComponent CreatePlayerComponent()
        {
            return new PlayerComponent() { Name = Faker.Name.FullName() };
        }

        public static TeamComponent CreateTeamComponent()
        {
            return new TeamComponent() { Name = Faker.Address.City() };
        }
    }
}
