using DL.ECS.Core.Components;

namespace DL.ECS.Core.Tests.TestComponents
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
