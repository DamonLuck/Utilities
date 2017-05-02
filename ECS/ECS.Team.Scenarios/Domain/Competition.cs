using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class Competition
    {
        private Context _context;
        private readonly ComponentFactory _componentFactory;

        public Competition(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }
    }
}
