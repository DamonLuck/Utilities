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
        private readonly ComponentFactory _componentFactory;

        public Teams(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }

        public void Create(int numberOfTeams, int playersPerTeam)
        {
            int skip = 0;
            IComponentBuilder<TeamComponent> builder = _componentFactory.TeamComponentBuilder();
            IEnumerable<IEntity> players = _context.GetEntitiesByComponent<PlayerComponent>();
            for (int i = 0; i < numberOfTeams; i++)
            { 
                IEntity team = _context.Create().AddComponent<TeamComponent>(builder.Build());
                team.AddComponent<TeamMembershipComponent>(
                    new TeamMembershipComponent(team.EntityId.Id, true));
                TeamMembershipComponent teamMembership =
                    new TeamMembershipComponent(team.EntityId.Id, false);
                players.Skip(skip).Take(playersPerTeam).ToList()
                .ForEach(x => x.AddComponent<TeamMembershipComponent>(teamMembership));
                skip += playersPerTeam;
            }
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
