using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;

namespace DL.ECS.Team.Scenarios.DomainRelations
{
    public class People
    {
        private Context _context;
        private readonly ComponentFactory _componentFactory;

        public People(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }
    }
}
