using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class PlayerModel
    {
        public long Id { get; set; }
        public long TeamId { get; set; }
        public string Name { get; set; }
        public bool IsCaptain { get; set; }
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

            return CreatePlayerModels(players, teamId);
        }

        private IEnumerable<IEntity> GetPlayers(long teamId)
        {
            return _context
                .GetEntitiesByComponent<TeamMembershipComponent>(x => x.TeamId == teamId
                && !x.IsTeam);
        }

        private IEnumerable<PlayerModel> CreatePlayerModels(IEnumerable<IEntity> entities, long teamId)
        {
            return entities
                .Where(x => x.GetComponent<PlayerComponent>() != null)
                .Select(x => CreatePlayerModel(teamId,
                    x.GetComponent<PlayerComponent>(),
                    x.GetComponent<TeamMembershipComponent>(), x));
        }

        private PlayerModel CreatePlayerModel(long teamId, PlayerComponent playerComponent, 
            TeamMembershipComponent teamMembershipComponent, 
            IEntity entity)
        {
            return new PlayerModel() {TeamId = teamId,
                Name = playerComponent.Name, Id = entity.EntityId.Id,
                IsCaptain = teamMembershipComponent.IsCaptain};
        }
    }
}
