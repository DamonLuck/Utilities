using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios.Domain
{
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
            IComponentBuilder builder = _componentFactory.PlayerComponentBuilder();

            for(int i=0; i< numberOfPlayers; i++)
                _context.Create().AddComponent(builder);
        }

        public IEnumerable<IEntity> GetAll()
        {
            return _context.GetEntitiesByComponent(
                _componentFactory.ComponentIds.PlayerComponentId);
        }
    }
}
