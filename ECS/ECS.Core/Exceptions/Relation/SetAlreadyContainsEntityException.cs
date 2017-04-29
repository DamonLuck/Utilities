namespace DL.ECS.Core.Exceptions
{
    public class SetAlreadyContainsEntityException : ECSException
    {
        public SetAlreadyContainsEntityException(long entityId)
        {
            EntityId = entityId;
            Message = $"Entity with id {entityId} already contained in set";
        }

        public new string Message { get; }
        public long EntityId { get; }
    }
}
