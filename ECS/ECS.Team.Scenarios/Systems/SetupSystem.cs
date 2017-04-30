using DL.ECS.Core;
using DL.ECS.Core.Systems;
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

        // Requires 
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
