using DL.ECS.Core.Components;

namespace DL.ECS.Core
{
    public interface IComponentBuilder<TComponent> where TComponent:IComponent
    {
        TComponent Build();
    }
}
