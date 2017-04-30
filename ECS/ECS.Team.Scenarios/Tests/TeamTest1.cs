using DL.ECS.Core;
using DL.ECS.Core.Components;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Components;
using System;

namespace DL.ECS.Team.Scenarios
{
    /// <summary>
    /// Creates two different component types, team and player
    /// Creates numerous entitis with either the team or player component
    /// 
    /// Tests GetEntitesByComponent and 
    /// </summary>
    public class TeamTest1 : ISystem
    {
        private ComponentId _playerComponentIndex;
        private ComponentId _teamComponentIndex;
        private Context _context;

        public TeamTest1()
        {
            int componentCount = 2;
            _playerComponentIndex = new ComponentId(0);
            _teamComponentIndex = new ComponentId(1);
            _context = new Context(componentCount);
        }

        public void Execute()
        {
            for (int i = 0; i < 5; i++)
            {
                IRelation set = _context.CreateSet();
                set.AddEntity(AddTeam(_context, _teamComponentIndex));

                for (int j = 0; j < 4; j++)
                {
                    set.AddEntity(AddPlayer(_context, _playerComponentIndex));
                }
            }

            for (int i = 0; i < 4; i++)
            {
                var relation = _context.GetRelationByRelationid(new RelationId(i));
                foreach (var entity in relation.GetEntities())
                    Console.WriteLine(entity);
                Console.WriteLine();
            }

            foreach (var entity in _context.GetEntitiesByComponent(_teamComponentIndex))
                Console.WriteLine(entity);

            Console.ReadLine();
        }

        static IEntity AddPlayer(Context context, ComponentId index)
        {
            PlayerComponent player = new PlayerComponent() { Name = Faker.Name.FullName() };
            return context.Create().AddComponent(player, index);
        }

        static IEntity AddTeam(Context context, ComponentId index)
        {
            TeamComponent team = new TeamComponent() { Name = Faker.Address.City() };
            return context.Create().AddComponent(team, index);
        }
    }
}
