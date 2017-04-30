using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.Infrastructure;
using System;

namespace DL.ECS.Team.Scenarios.Systems
{

    public class ResultSystem : ISystem
    {
        public ResultSystem(Context context)
        {
        }

        public void Execute()
        {
            AmbientLogger.SystemNotification.ResultSystem();
        }

    }
}
