using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class League
    {
        private Context _context;
        private readonly ComponentFactory _componentFactory;

        public League(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }

        public void Create(int numberOfLeagues, int numberOfTeamsPerLeague)
        {
            IComponentBuilder builder = _componentFactory.LeagueComponentBuilder();
            IEnumerable<IEntity> teams = _context.GetEntitiesByComponent(
                _componentFactory.ComponentIds.TeamComponentId);
            for (int i = 0; i < numberOfLeagues; i++)
            { 
                IEntity league = _context.Create().AddComponent(builder);
                _context.CreateSet()
                    .AddPrimaryEntity(league)
                    .AddEntities(teams.Take(numberOfTeamsPerLeague));
            }
        }

        public IEnumerable<IEntity> GetAll()
        {
            return _context.GetEntitiesByComponent(
                _componentFactory.ComponentIds.LeagueComponentId);
        }
    }
}
