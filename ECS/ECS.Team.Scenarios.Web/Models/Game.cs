using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;
using DL.ECS.Team.Scenarios.Systems;

namespace ECS.Team.Scenarios.Web.Models
{
    public class Game
    {
        static Game()
        {
            Context = new DomainContext();
            SetupSystem setupSystem =
                new SetupSystem(Context);
            setupSystem.Execute();
        }

        public static DomainContext Context { get; }
    }
}