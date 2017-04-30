using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class TeamComponent : IComponent
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Team {Name}";
        }
    }
}
