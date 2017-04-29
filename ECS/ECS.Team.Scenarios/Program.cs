using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios
{
    class Program
    {
        static void Main(string[] args)
        {
            int componentCount = 2;
            int playerComponentIndex = 1;
            int teamComponentIndex = 1;
            Context context = new Context(componentCount);

            for (int i = 0; i < 5; i++)
            {
                IRelation set = context.CreateSet();
                set.AddEntity(AddTeam(context, teamComponentIndex));

                for (int j = 0; j < 4; j++)
                {
                    set.AddEntity(AddPlayer(context, playerComponentIndex));
                }

            }

            for(int i=0; i<4; i++)
            { 
                foreach (var entity in context.GetSetEntities(new RelationId(i)))
                    Console.WriteLine(entity);
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        static IEntity AddPlayer(Context context, int index)
        {
            PlayerComponent player = new PlayerComponent() { Name = Faker.Name.FullName() };
            return context.Create().AddComponent(player, index);
        }

        static IEntity AddTeam(Context context, int index)
        {
            TeamComponent team = new TeamComponent() { Name = Faker.Address.City() };
            return context.Create().AddComponent(team, index);
        }
    }
}
