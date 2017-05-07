namespace DL.ECS.Team.Scenarios.Domain
{
    public class FixturesTeamModel
    {
        public FixturesTeamModel(long teamId, string teamName, int goals)
        {
            TeamId = teamId;
            TeamName = teamName;
            Goals = goals;
        }

        public long TeamId { get; }
        public string TeamName { get; }
        public int Goals { get; }
    }
}
