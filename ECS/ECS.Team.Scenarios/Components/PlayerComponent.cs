using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerComponent : IComponent
    {
        public PlayerComponent(string name, int handling, int tacking, int passing, int shooting, int stamina)
        {
            Name = name;
            Handling = handling;
            Tacking = tacking;
            Passing = passing;
            Shooting = shooting;
            Stamina = stamina;
        }

        public string Name { get; }
        public int Handling { get; }
        public int Tacking { get; }
        public int Passing { get; }
        public int Shooting { get; }
        public int Stamina { get; }

        public override string ToString()
        {
            return $"Player {Name}";
        }
    }
}
