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

        public Players(Context context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.Create().AddComponent(
                ComponentFactory.CreatePlayerComponent());
        }

        public IEnumerable<PlayerModel> GetAll(long teamId)
        {
            IEnumerable<IEntity> players = GetPlayers(teamId);

            return CreatePlayerModels(players);
        }

        private IEnumerable<IEntity> GetPlayers(long teamId)
        {
            return _context
                .GetEntitiesByComponent<TeamMembershipComponent>(x => x.TeamId == teamId
                && !x.IsTeam);
        }

        private IEnumerable<PlayerModel> CreatePlayerModels(IEnumerable<IEntity> entities)
        {
            return entities
                .Where(x => x.GetComponent<PlayerComponent>() != null)
                .Select(x => CreatePlayerModel(
                    x.GetComponent<PlayerComponent>(), x));
        }

        private PlayerModel CreatePlayerModel(PlayerComponent playerComponent, IEntity entity)
        {
            return new PlayerModel() { Name = playerComponent.Name, Id = entity.EntityId.Id };
        }
    }
}
