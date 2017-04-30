using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.Infrastructure;
using System;

namespace DL.ECS.Team.Scenarios.Systems
{

    public class FixtureSystem: ISystem
    {
        public FixtureSystem(Context context)
        {
        }

        public void Execute()
        {
            AmbientLogger.SystemNotification.FixtureSystem();
        }
    }
}
