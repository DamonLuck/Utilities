using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.ECS.Team.Scenarios.Systems
{
    public class MatchSystem : ISystem
    {
        private readonly DomainContext _context;
        public static int CurrentTurn = 1;
        private Random _random = new Random();
        public MatchSystem(DomainContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            IEnumerable<IEntity> gameEntities = _context.GetEntitiesByComponent<MatchMembershipComponent>
                (x => x.GameTurn == CurrentTurn);

            foreach(var gameEntity in gameEntities)
            {
                MatchMembershipComponent match = gameEntity.GetComponent<MatchMembershipComponent>();
                if (match.IsGamePlayed)
                    throw new InvalidOperationException("Cannot play game that has already been played");

                // Play match
                gameEntity.ReplaceComponent(new MatchMembershipComponent
                    (match.GameTurn, match.LeagueId, match.HomeTeamId, match.AwayTeamId,
                        _random.Next(4), _random.Next(4), true));
                match = gameEntity.GetComponent<MatchMembershipComponent>();

                // Update league data
                // Get team entity
                IEntity homeTeam = _context.GetEntityById(new EntityId(match.HomeTeamId));
                IEntity awayTeam = _context.GetEntityById(new EntityId(match.AwayTeamId));
                var homeLeagueMembership= homeTeam.GetComponent<LeagueMembershipComponent>();
                var awayLeagueMembership = awayTeam.GetComponent<LeagueMembershipComponent>();

                if(match.HomeGoals > match.AwayGoals)
                {
                    homeTeam.ReplaceComponent(new LeagueMembershipComponent(
                        homeLeagueMembership.LeagueId, homeLeagueMembership.IsLeague, 
                            homeLeagueMembership.Won + 1, homeLeagueMembership.Lost, 
                            homeLeagueMembership.Draw, homeLeagueMembership.GF + match.HomeGoals, 
                            homeLeagueMembership.GA + match.AwayGoals));
                    awayTeam.ReplaceComponent(new LeagueMembershipComponent(
                        awayLeagueMembership.LeagueId, awayLeagueMembership.IsLeague,
                            awayLeagueMembership.Won , awayLeagueMembership.Lost + 1,
                            awayLeagueMembership.Draw, awayLeagueMembership.GF + match.AwayGoals,
                            awayLeagueMembership.GA + match.HomeGoals));
                }
                else if (match.HomeGoals == match.AwayGoals)
                {
                    homeTeam.ReplaceComponent(new LeagueMembershipComponent(
                        homeLeagueMembership.LeagueId, homeLeagueMembership.IsLeague,
                            homeLeagueMembership.Won , homeLeagueMembership.Lost,
                            homeLeagueMembership.Draw + 1, homeLeagueMembership.GF + match.HomeGoals,
                            homeLeagueMembership.GA + match.AwayGoals));
                    awayTeam.ReplaceComponent(new LeagueMembershipComponent(
                        awayLeagueMembership.LeagueId, awayLeagueMembership.IsLeague,
                            awayLeagueMembership.Won, awayLeagueMembership.Lost ,
                            awayLeagueMembership.Draw + 1, awayLeagueMembership.GF + match.AwayGoals,
                            awayLeagueMembership.GA + match.HomeGoals));
                }
                else
                {
                    homeTeam.ReplaceComponent(new LeagueMembershipComponent(
                        homeLeagueMembership.LeagueId, homeLeagueMembership.IsLeague,
                            homeLeagueMembership.Won , homeLeagueMembership.Lost + 1,
                            homeLeagueMembership.Draw, homeLeagueMembership.GF + match.HomeGoals,
                            homeLeagueMembership.GA + match.AwayGoals));
                    awayTeam.ReplaceComponent(new LeagueMembershipComponent(
                        awayLeagueMembership.LeagueId, awayLeagueMembership.IsLeague,
                            awayLeagueMembership.Won + 1, awayLeagueMembership.Lost,
                            awayLeagueMembership.Draw , awayLeagueMembership.GF + match.AwayGoals,
                            awayLeagueMembership.GA + match.HomeGoals));
                }
            }

            CurrentTurn += 1;
        }

    }
}
