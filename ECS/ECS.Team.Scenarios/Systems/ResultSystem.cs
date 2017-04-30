using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Domain;
using DL.Infrastructure;
using System;

namespace DL.ECS.Team.Scenarios.Systems
{

    public class ResultSystem : ISystem
    {
        public ResultSystem(DomainContext context)
        {
        }

        public void Execute()
        {
            AmbientLogger.SystemNotification.ResultSystem();
        }

    }
}
