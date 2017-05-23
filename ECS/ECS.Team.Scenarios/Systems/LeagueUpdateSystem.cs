using DL.ECS.Core.Entity;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;

namespace DL.ECS.Team.Scenarios.Systems
{
    public class LeagueUpdateSystem : ISystem
    {
        private readonly DomainContext _context;
        public LeagueUpdateSystem(DomainContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            // Update league data
            // Get team entity
            foreach (var matchResultEntity in _context.GetEntitiesByComponent<MatchResultComponent>())
            {
                MatchResultComponent matchResult = matchResultEntity.GetComponent<MatchResultComponent>();
                IEntity homeTeam = _context.GetEntityById(new EntityId(matchResult.HomeTeamId));
                IEntity awayTeam = _context.GetEntityById(new EntityId(matchResult.AwayTeamId));
                var homeLeagueMembership = homeTeam.GetComponent<LeagueMembershipComponent>();
                var awayLeagueMembership = awayTeam.GetComponent<LeagueMembershipComponent>();

                if (matchResult.HomeGoals > matchResult.AwayGoals)
                {
                    homeTeam.ReplaceComponent(new LeagueMembershipComponent(
                        homeLeagueMembership.LeagueId,
                            homeLeagueMembership.Won + 1, homeLeagueMembership.Lost,
                            homeLeagueMembership.Draw, homeLeagueMembership.Gf + matchResult.HomeGoals,
                            homeLeagueMembership.Ga + matchResult.AwayGoals));
                    awayTeam.ReplaceComponent(new LeagueMembershipComponent(
                        awayLeagueMembership.LeagueId,
                            awayLeagueMembership.Won, awayLeagueMembership.Lost + 1,
                            awayLeagueMembership.Draw, awayLeagueMembership.Gf + matchResult.AwayGoals,
                            awayLeagueMembership.Ga + matchResult.HomeGoals));
                }
                else if (matchResult.HomeGoals == matchResult.AwayGoals)
                {
                    homeTeam.ReplaceComponent(new LeagueMembershipComponent(
                        homeLeagueMembership.LeagueId,
                            homeLeagueMembership.Won, homeLeagueMembership.Lost,
                            homeLeagueMembership.Draw + 1, homeLeagueMembership.Gf + matchResult.HomeGoals,
                            homeLeagueMembership.Ga + matchResult.AwayGoals));
                    awayTeam.ReplaceComponent(new LeagueMembershipComponent(
                        awayLeagueMembership.LeagueId,
                            awayLeagueMembership.Won, awayLeagueMembership.Lost,
                            awayLeagueMembership.Draw + 1, awayLeagueMembership.Gf + matchResult.AwayGoals,
                            awayLeagueMembership.Ga + matchResult.HomeGoals));
                }
                else
                {
                    homeTeam.ReplaceComponent(new LeagueMembershipComponent(
                        homeLeagueMembership.LeagueId,
                            homeLeagueMembership.Won, homeLeagueMembership.Lost + 1,
                            homeLeagueMembership.Draw, homeLeagueMembership.Gf + matchResult.HomeGoals,
                            homeLeagueMembership.Ga + matchResult.AwayGoals));
                    awayTeam.ReplaceComponent(new LeagueMembershipComponent(
                        awayLeagueMembership.LeagueId,
                            awayLeagueMembership.Won + 1, awayLeagueMembership.Lost,
                            awayLeagueMembership.Draw, awayLeagueMembership.Gf + matchResult.AwayGoals,
                            awayLeagueMembership.Ga + matchResult.HomeGoals));
                }

                _context.Destroy(matchResultEntity);
            }
        }
    }
}
