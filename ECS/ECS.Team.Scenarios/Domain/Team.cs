using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class TeamModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int P
        {
            get
            {
                return W + D + L;
            }
        }
        public int Pts
        {
            get
            {
                return W * 3 + D * 1;
            }
        }

        public int W { get; set; }
        public int D { get; set; }
        public int L { get; set; }
        public int GF { get; set; }
        public int GA { get; set; }

    }

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
                .AddComponent(ComponentFactory.CreateTeamComponent())
                .CreateTeamMembershipComponent();

            AddPlayers(team, players);
        }

        private static void AddPlayers(IEntity team, IEnumerable<IEntity> players)
        {
            TeamMembershipComponent teamMembership =
                ComponentFactory.CreateTeamPlayerMembershipComponent(team);
            players.ToList()
                .ForEach(x => x.AddComponent(teamMembership));
        }

        public void SetTeamCaptain(long teamId, long playerId)
        {
            // Remove current captain if required
            var currentCaptain = _context.GetEntitiesByComponent<TeamMembershipComponent>
                (x => !x.IsTeam && x.TeamId == teamId && x.IsCaptain).FirstOrDefault();
            if (currentCaptain != null && currentCaptain.EntityId.Id == playerId)
                return; // Correct player is captain already
            else if (currentCaptain != null)
            {
                var teamMembership = currentCaptain.GetComponent<TeamMembershipComponent>();
                currentCaptain.ReplaceComponent(CreateTeamMembershipComponent(teamMembership, false));
                // Change this player to not be captain
            }
            var newCaptain = _context.GetEntitiesByComponent<TeamMembershipComponent>
                (x => !x.IsTeam && x.TeamId == teamId)
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
                teamMembershipComponent.IsTeam,
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
                .GetEntitiesByComponent<LeagueMembershipComponent>(x => x.LeagueId == leagueId
                    && !x.IsLeague);
        }

        private IEnumerable<TeamModel> CreateTeamModels(IEnumerable<IEntity> entities)
        {
            return entities
                .Where(x => x.GetComponent<TeamComponent>() != null)
                .Select(x => CreateTeamModel(
                    x.GetComponent<TeamComponent>(),
                    x.GetComponent<LeagueMembershipComponent>(), x))
                    .OrderBy(x=>x.Pts)
                    .ThenBy(x=>x.Name);
        }

        private TeamModel CreateTeamModel(TeamComponent teamComponent,
            LeagueMembershipComponent leagueMembershipComponent,
            IEntity entity)
        {
            return new TeamModel() { Name = teamComponent.Name,
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
