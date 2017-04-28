using System;

namespace DL.ECS.Core.Exceptions
{
    public class EntityException : ECSException
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
