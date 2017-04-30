using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Domain;
using System;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios.Systems
{
    public enum GameState
    {
        Setup,
        Fixture,
        Match,
        Result
    }

    public class GameStateSystem : ISystem
    {
        private GameState _gameState;
        private SetupSystem _setupSystem;
        private DomainContext _context;
        public GameStateSystem(DomainContext context)
        {
            _context = context;
            _setupSystem = new SetupSystem(context);
            _gameState = GameState.Setup;
            _setupSystem.Execute();
        }

        public void Execute(string input)
        {
            if(input == "l")
            {
                // list leagues
                WriteEntitiesToConsole(_context.League.GetAll());
                return;
            }
            else if(input == "t")
            {
                // list teams
                WriteEntitiesToConsole(_context.Teams.GetAll());
                return;
            }
            int entityId = -1;
            int.TryParse(input, out entityId);

            if(entityId != -1)
            {
                // Check set primary keys first
                IRelation relation = _context.GetRelationByPrimaryKey(new EntityId(entityId));
                if(relation != null)
                {
                    WriteEntitiesToConsole(relation.GetEntities());
                }
                else
                {
                    Console.WriteLine("Unknown Id");
                }
            }
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        private void WriteEntitiesToConsole(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
                Console.WriteLine(entity);
        }
    }
}
