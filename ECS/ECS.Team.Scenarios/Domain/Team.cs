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

        public void SetTeamCaptain(EntityId teamId, EntityId playerId)
        {

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
                    x.GetComponent<TeamComponent>(), x));
        }

        private TeamModel CreateTeamModel(TeamComponent teamComponent, IEntity entity)
        {
            return new TeamModel() { Name = teamComponent.Name, Id = entity.EntityId.Id };
        }
    }
}
