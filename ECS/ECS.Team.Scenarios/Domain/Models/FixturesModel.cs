using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class FixturesModel
    {
        public FixturesModel(FixturesTeamModel homeTeam, FixturesTeamModel awayTeam, bool isGamePlayed)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            IsGamePlayed = isGamePlayed;
        }

        public bool IsGamePlayed { get; }
        public FixturesTeamModel HomeTeam { get; }
        public FixturesTeamModel AwayTeam { get; }
    }
}
