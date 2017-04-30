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
            Console.WriteLine("Enter l to list leagues and t to list teams");
            Console.WriteLine("Enter entity id to list contents of entities set");
            string inputString = "";
            inputString = Console.ReadLine();
            while (inputString != "q")
            {
                gameStateSystem.Execute(inputString);
                inputString = Console.ReadLine();
            }
        }

    }
}
