using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;
using DL.ECS.Team.Scenarios.Systems;
using System;

namespace DL.ECS.Team.Scenarios
{

    class Program
    {
        static void Main(string[] args)
        {
            ComponentFactory factory = new ComponentFactory();
            GameStateSystem gameStateSystem =
                new GameStateSystem(new DomainContext(factory));

            Console.WriteLine("Enter q to quit. Return to execute next step");
            while(Console.ReadLine() != "q")
                gameStateSystem.Execute();
        }

    }
}
