using DL.ECS.Core;

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
