using DL.ECS.Core.Entity;

namespace DL.ECS.Core.Exceptions.Entity
{
    public class EntityDoesNotHaveComponentException : EcsException
    {
        public EntityDoesNotHaveComponentException(IEntity entity, long index)
        {
            Entity = entity;
            Index = index;
            Message = $"Entity with id {entity.EntityId} does not have component with index {index}";
        }

        public new string Message { get; }
        public IEntity Entity { get; }
        public long Index { get; }
    }
}
