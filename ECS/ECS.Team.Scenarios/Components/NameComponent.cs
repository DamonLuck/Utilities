using DL.ECS.Core.Components;

namespace DL.ECS.Team.Scenarios.Components
{
    public class NameComponent : IComponent
    {
        public NameComponent(string name)
        {
            Name = name;
        }
        public string Name { get; }

    }
}
