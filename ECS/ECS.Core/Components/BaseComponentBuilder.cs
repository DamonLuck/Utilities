using DL.ECS.Core.Components;

namespace DL.ECS.Core
{
    public abstract class BaseComponentBuilder : IComponentBuilder
    {
        public BaseComponentBuilder(ComponentId componentId)
        {
            Index = componentId;
        }

        public abstract IComponent Build();

        public ComponentId Index { get; }
    }
}
