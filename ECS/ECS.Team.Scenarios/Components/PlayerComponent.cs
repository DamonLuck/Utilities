using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerComponent : IComponent
    {
        public PlayerComponent(int handling, int tacking, int passing, int shooting, int stamina)
        {
            Handling = handling;
            Tacking = tacking;
            Passing = passing;
            Shooting = shooting;
            Stamina = stamina;
        }

        public int Handling { get; }
        public int Tacking { get; }
        public int Passing { get; }
        public int Shooting { get; }
        public int Stamina { get; }
    }
}
