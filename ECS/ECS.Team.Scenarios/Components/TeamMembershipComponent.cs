using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class TeamMembershipComponent : IComponent
    {
        public TeamMembershipComponent(long teamId, bool isCaptain = false)
        {
            TeamId = teamId;
            IsCaptain = isCaptain;
        }

        public long TeamId { get; }
        public bool IsCaptain { get; }
    }
}
