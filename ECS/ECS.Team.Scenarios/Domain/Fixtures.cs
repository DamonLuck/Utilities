using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class Fixtures
    {
        private Context _context;

        public Fixtures(Context context)
        {
            _context = context;
        }

        public RoundModel GetRound(long leagueId, int roundNumber)
        {
            return new RoundModel(GetFixtures(leagueId, roundNumber), roundNumber, leagueId);
        }

        private IEnumerable<FixturesModel> GetFixtures(long leagueId, int roundNumber)
        {
            return _context.GetInstancesOfComponent<MatchMembershipComponent>
                (x => x.LeagueId == leagueId && x.GameTurn == roundNumber)
                .Select(x => CreateFixtureModel(x));
        }

        private FixturesModel CreateFixtureModel(MatchMembershipComponent matchMembership)
        {
            var homeTeam = _context.GetEntityById(new EntityId(matchMembership.HomeTeamId));
            var awayTeam = _context.GetEntityById(new EntityId(matchMembership.AwayTeamId));
            FixturesTeamModel homeTeamModel = new FixturesTeamModel (
                homeTeam.EntityId.Id,
                homeTeam.GetComponent<NameComponent>().Name,
                matchMembership.HomeGoals);
            FixturesTeamModel awayTeamModel = new FixturesTeamModel(
                awayTeam.EntityId.Id,
                awayTeam.GetComponent<NameComponent>().Name,
                matchMembership.AwayGoals);
            return new FixturesModel(homeTeamModel, awayTeamModel, matchMembership.IsGamePlayed);
        }
    }
}
