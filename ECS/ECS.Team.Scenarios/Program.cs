using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Systems;
using DL.Infrastructure;
using System;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios
{

    class Program
    {
        static void Main(string[] args)
        {
            GameStateSystem gameStateSystem =
                new GameStateSystem(
                    ComponentFactory.CreateContext());

            Console.WriteLine("Enter q to quit. Return to execute next step");
            while(Console.ReadLine() != "q")
                gameStateSystem.Execute();
        }

    }
}
