using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PlayerComponent : IComponent
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Player {Name}";
        }
    }
}
