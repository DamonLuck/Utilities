using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class TeamMembershipComponent : IComponent
    {
        public TeamMembershipComponent(long teamId, bool isTeam, bool isCaptain = false)
        {
            TeamId = teamId;
            IsTeam = isTeam;
            IsCaptain = isCaptain;
        }

        public long TeamId { get; }
        public bool IsTeam { get; }
        public bool IsCaptain { get; }
    }
}
