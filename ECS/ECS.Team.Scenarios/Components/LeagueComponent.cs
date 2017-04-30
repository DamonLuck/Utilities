using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class LeagueComponent : IComponent
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"League {Name}";
        }
    }
}
