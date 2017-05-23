using DL.ECS.Core.Components;
using DL.ECS.Core.Entity;

namespace DL.ECS.Core.Exceptions.Entity
{

    public class EntityAlreadyHasComponentException : EcsException
    {
        public EntityAlreadyHasComponentException(IEntity entity, IComponent component, long index)
        {
            Entity = entity;
            Component = component;
            Index = index;
            Message = $"Entity with id {entity.EntityId} already has component with index {index}";
        }

        public new string Message { get; }
        public IEntity Entity { get; }
        public IComponent Component { get; }
        public long Index { get; }
    }
}
