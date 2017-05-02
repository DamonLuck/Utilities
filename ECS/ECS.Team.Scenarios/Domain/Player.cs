using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class PlayerModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class Players
    {
        private Context _context;
        private readonly ComponentFactory _componentFactory;

        public Players(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }

        public void Create(int numberOfPlayers)
        {
            IComponentBuilder<PlayerComponent> builder = 
                _componentFactory.PlayerComponentBuilder();

            for(int i=0; i< numberOfPlayers; i++)
                _context.Create().AddComponent<PlayerComponent>(builder.Build());
        }

        public IEnumerable<PlayerModel> GetAll(long teamId)
        {
            IRelation leagueRelation =
                _context.GetRelationsBy(SetFunctions.LookupByPrimaryEntity(new EntityId(teamId)))
                .Single();

            return leagueRelation.GetEntities()
                .Where(x => x.GetComponent<PlayerComponent>() != null)
                .Select(x => CreateTeamModel(
                    x.GetComponent<PlayerComponent>(), x));
        }

        private PlayerModel CreateTeamModel(PlayerComponent playerComponent, IEntity entity)
        {
            return new PlayerModel() { Name = playerComponent.Name, Id = entity.EntityId.Id };
        }
    }
}
