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
            IComponentBuilder builder = _componentFactory.TeamComponentBuilder();
            IEnumerable<IEntity> players = _context.GetEntitiesByComponent(
                _componentFactory.ComponentIds.PlayerComponentId);
            for (int i = 0; i < numberOfTeams; i++)
            { 
                IEntity team = _context.Create().AddComponent<TeamComponent>(builder);
                _context.CreateSet()
                    .AddPrimaryEntity(team)
                    .AddEntities(players.Take(playersPerTeam));
            }
        }

        public void SetTeamCaptain(EntityId teamId, EntityId playerId)
        {
            IComponentBuilder builder = _componentFactory.PlayerComponentBuilder();
            IComponentBuilder playerCaptainBuilder = _componentFactory.PlayerCaptainComponentBuilder();
            var teamRelation = _context.GetRelationsBy(
                SetFunctions.LookupByPrimaryEntity(teamId))
                .Single();
            foreach(var teamEntity in teamRelation.GetEntities())
            {
                teamEntity.RemoveComponent(_componentFactory.ComponentIds.PlayerCaptainComponentId);
                if (teamEntity.EntityId == playerId)
                    teamEntity.AddComponent<PlayerCaptainComponent>(playerCaptainBuilder);
            }
        }

        public IEnumerable<TeamModel> GetAll(long leagueId)
        {
            IRelation leagueRelation =
                _context.GetRelationsBy(SetFunctions.LookupByPrimaryEntity(new EntityId(leagueId)))
                .Single();

            return leagueRelation.GetEntities()
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
