using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios.Domain.Models
{
    public class RoundModel
    {
        public RoundModel(IEnumerable<FixturesModel> fixtures, int roundNumber, long leagueId)
        {
            Fixtures = fixtures;
            RoundNumber = roundNumber;
            LeagueId = leagueId;
        }

        public long LeagueId { get; }
        public int RoundNumber { get; }
        public IEnumerable<FixturesModel> Fixtures { get; set; }

    }
}
