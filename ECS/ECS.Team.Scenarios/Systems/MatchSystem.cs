using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Domain;
using DL.Infrastructure;
using System;

namespace DL.ECS.Team.Scenarios.Systems
{
    public class MatchSystem : ISystem
    {
        public MatchSystem(DomainContext context)
        {
        }

        public void Execute()
        {
            AmbientLogger.SystemNotification.MatchSystem();
        }
    }
}
