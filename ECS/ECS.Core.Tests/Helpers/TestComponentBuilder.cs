using DL.ECS.Core.Components;

namespace DL.ECS.Core.Tests
{
    public class TestComponentBuilder : IComponentBuilder
    {
        private IComponent _component;
        public TestComponentBuilder(ComponentId componentId,
            IComponent component)
        {
            Index = componentId;
            _component = component;
        }

        public ComponentId Index { get; }

        public IComponent Build() => _component;
    }
}
