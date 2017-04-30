using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
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
                IEntity team = _context.Create().AddComponent(builder);
                _context.CreateSet()
                    .AddPrimaryEntity(team)
                    .AddEntities(players.Take(playersPerTeam));
            }
        }

        public IEnumerable<IEntity> GetAll()
        {
            return _context.GetEntitiesByComponent(
                _componentFactory.ComponentIds.TeamComponentId);
        }
    }
}
