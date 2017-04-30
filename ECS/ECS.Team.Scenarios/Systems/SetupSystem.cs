using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.Infrastructure;
using System;

namespace DL.ECS.Team.Scenarios.Systems
{

    public class SetupSystem : ISystem
    {
        private readonly Context _context;

        public SetupSystem(Context context)
        {
            _context = context;
        }

        public void Execute()
        {
            AmbientLogger.SystemNotification.SystemSetup();
        }
    }
}
