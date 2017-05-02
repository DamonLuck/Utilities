using DL.ECS.Core;

namespace DL.ECS.Team.Scenarios.Components
{
    public class TeamMembershipComponent : IComponent
    {
        public TeamMembershipComponent(long teamId, bool isTeam)
        {
            TeamId = teamId;
            IsTeam = isTeam;
        }

        public long TeamId { get; }
        public bool IsTeam { get; }
    }
}
