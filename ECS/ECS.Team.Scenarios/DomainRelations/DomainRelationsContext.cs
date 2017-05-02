using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.ECS.Team.Scenarios.DomainRelations
{
    public class DomainRelationsContext : Context
    {
        public DomainRelationsContext(ComponentFactory componentFactory)
            :base(componentFactory.ComponentLookup)
        {
            People = new People(this, componentFactory);
        }

        public People People { get; }
    }
}
