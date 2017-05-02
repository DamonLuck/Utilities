using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PersonComponent : IComponent
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
