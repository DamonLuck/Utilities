using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class Team
    {
        private Context _context;
        private readonly ComponentFactory _componentFactory;

        public Team(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }

        public IEntity CreateTeam()
        {
            IComponentBuilder builder = _componentFactory.TeamComponentBuilder();
            return _context.Create()
                .AddComponent(builder);
        }
    }
}
