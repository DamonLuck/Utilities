using DL.ECS.Core.Components;

namespace DL.ECS.Core.Tests.TestComponents
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