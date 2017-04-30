using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

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
    }
}
