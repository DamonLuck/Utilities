using DL.ECS.Core.Components;

namespace DL.ECS.Core.Tests
{
    public class TestComponentBuilder<TComponent> : IComponentBuilder<TComponent> 
        where TComponent : IComponent
    {
        private TComponent _component;
        public TestComponentBuilder(TComponent component)
        {
            _component = component;
        }

        public TComponent Build() => _component;
    }
}
