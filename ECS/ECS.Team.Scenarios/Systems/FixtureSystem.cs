using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Domain;
using DL.Infrastructure;

namespace DL.ECS.Team.Scenarios.Systems
{

    public class FixtureSystem: ISystem
    {
        public FixtureSystem(DomainContext context)
        {
        }

        public void Execute()
        {
            AmbientLogger.SystemNotification.FixtureSystem();
        }
    }
}
