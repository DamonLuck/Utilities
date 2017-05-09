using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{

    public class Teams
    {
        private Context _context;

        public Teams(Context context)
        {
            _context = context;
        }

        public void Create(IEnumerable<IEntity> players)
        {
            IEntity team = _context.Create()
                .CreateTeamComponent()
                .CreateTeamMembershipComponent();

            AddPlayers(team, players);
        }

        private static void AddPlayers(IEntity team, IEnumerable<IEntity> players)
        {
            players.ToList()
                .ForEach(x => x.CreateTeamPlayerMembershipComponent(team));
        }

        public void SetTeamCaptain(long teamId, long playerId)
        {
            // Remove current captain if required
            var currentCaptain = _context.GetEntitiesByComponent<TeamMembershipComponent>
                (x => x.TeamId == teamId && x.IsCaptain).FirstOrDefault();
            if (currentCaptain != null && currentCaptain.EntityId.Id == playerId)
                return; // Correct player is captain already
            else if (currentCaptain != null)
            {
                var teamMembership = currentCaptain.GetComponent<TeamMembershipComponent>();
                currentCaptain.ReplaceComponent(CreateTeamMembershipComponent(teamMembership, false));
                // Change this player to not be captain
            }
            var newCaptain = _context.GetEntitiesByComponent<TeamMembershipComponent>
                (x => x.TeamId == teamId)
                .Where(x => x.EntityId.Id == playerId).FirstOrDefault();

            if(newCaptain != null)
            { 
                var newCaptainMembership = newCaptain.GetComponent<TeamMembershipComponent>();
                newCaptain.ReplaceComponent(CreateTeamMembershipComponent(newCaptainMembership, true));
            }
        }

        private TeamMembershipComponent CreateTeamMembershipComponent(TeamMembershipComponent teamMembershipComponent , bool isCaptain)
        {
            return new TeamMembershipComponent(
                teamMembershipComponent.TeamId,
                isCaptain);
        }

        public IEnumerable<TeamModel> GetAll(long leagueId)
        {
            IEnumerable<IEntity> teams = GetTeams(leagueId);

            return CreateTeamModels(teams);
        }

        private IEnumerable<IEntity> GetTeams(long leagueId)
        {
            return _context
                .GetEntitiesByComponent<LeagueMembershipComponent>(x => x.LeagueId == leagueId);
        }

        private IEnumerable<TeamModel> CreateTeamModels(IEnumerable<IEntity> entities)
        {
            return entities
                .Where(x => x.GetComponent<TeamComponent>() != null)
                .Select(x => CreateTeamModel(
                    x.GetComponent<TeamComponent>(),
                    x.GetComponent<NameComponent>(),
                    x.GetComponent<LeagueMembershipComponent>(), x))
                    .OrderByDescending(x=>x.Pts)
                    .ThenBy(x=>x.Name);
        }

        private TeamModel CreateTeamModel(TeamComponent teamComponent,
            NameComponent nameComponent,
            LeagueMembershipComponent leagueMembershipComponent,
            IEntity entity)
        {
            return new TeamModel() { Name = nameComponent.Name,
                Id = entity.EntityId.Id,
                W = leagueMembershipComponent.Won,
                D = leagueMembershipComponent.Draw,
                L = leagueMembershipComponent.Lost,
                GF = leagueMembershipComponent.GF,
                GA = leagueMembershipComponent.GA
            };
        }
    }
}
