namespace DL.ECS.Team.Scenarios.Domain
{
    public class FixturesTeamModel
    {
        public FixturesTeamModel(long teamId, string teamName)
        {
            TeamId = teamId;
            TeamName = teamName;
        }

        public long TeamId { get; }
        public string TeamName { get; }
    }
}
