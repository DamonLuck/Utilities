using DL.ECS.Core.Components;

namespace DL.ECS.Core.Tests
{
    public class TestComponentBuilder : IComponentBuilder
    {
        private IComponent _component;
        public TestComponentBuilder(IComponent component)
        {
            _component = component;
        }

        public IComponent Build() => _component;
    }
}
