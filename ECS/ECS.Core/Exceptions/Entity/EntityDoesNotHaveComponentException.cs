namespace DL.ECS.Core.Exceptions
{
    public class EntityDoesNotHaveComponentException : ECSException
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
