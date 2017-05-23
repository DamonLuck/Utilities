using DL.ECS.Core.Entity;

namespace DL.ECS.Core.Exceptions
{
    public class EntityException : EcsException
    {
        public EntityException(string message, IEntity entity)
        {
            Message = message;
            Entity = entity;
        }
        public new string Message { get; }
        public IEntity Entity { get; }
    }
}
