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
    }

    public class Teams
    {
        private Context _context;

        public Teams(Context context)
        {
            _context = context;
        }

        public void Create(int numberOfTeams, int playersPerTeam)
        {
            int skip = 0;
            IEnumerable<IEntity> players = _context.GetEntitiesByComponent<PlayerComponent>();
            for (int i = 0; i < numberOfTeams; i++)
            {
                IEntity team = _context.Create()
                    .AddComponent(ComponentFactory.CreateTeamComponent())
                    .CreateTeamMembershipComponent();
                TeamMembershipComponent teamMembership =
                    ComponentFactory.CreateTeamPlayerMembershipComponent(team);

                AddPlayers(playersPerTeam, skip, players, teamMembership);
                skip += playersPerTeam;
            }
        }

        private static void AddPlayers(int playersPerTeam, int skip, IEnumerable<IEntity> players, TeamMembershipComponent teamMembership)
        {
            players.Skip(skip).Take(playersPerTeam).ToList()
                .ForEach(x => x.AddComponent(teamMembership));
        }

        public void SetTeamCaptain(EntityId teamId, EntityId playerId)
        {

        }

        public IEnumerable<TeamModel> GetAll(long leagueId)
        {
            var entities = _context.GetEntitiesByComponent<LeagueMembershipComponent>()
                .Where(x => x.GetComponent<LeagueMembershipComponent>().LeagueId == leagueId);

            return entities
                .Where(x => x.GetComponent<TeamComponent>() != null)
                .Select(x => CreateTeamModel(
                    x.GetComponent<TeamComponent>(), x));
        }

        private TeamModel CreateTeamModel(TeamComponent teamComponent, IEntity entity)
        {
            return new TeamModel() { Name = teamComponent.Name, Id = entity.EntityId.Id };
        }
    }
}
