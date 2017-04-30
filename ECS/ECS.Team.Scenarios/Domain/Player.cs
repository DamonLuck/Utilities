using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class Player
    {
        private Context _context;
        private readonly ComponentFactory _componentFactory;

        public Player(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }

        public IEntity CreatePlayer()
        {
            IComponentBuilder builder = _componentFactory.PlayerComponentBuilder();
            return _context.Create()
                .AddComponent(builder);
        }
    }
}
