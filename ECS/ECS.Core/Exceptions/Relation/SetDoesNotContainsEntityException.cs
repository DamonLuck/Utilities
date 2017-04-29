namespace DL.ECS.Core.Exceptions
{
    public class SetDoesNotContainsEntityException : ECSException
    {
        public SetDoesNotContainsEntityException(long entityId)
        {
            EntityId = entityId;
            Message = $"Entity with id {entityId} does not exist in set";
        }

        public new string Message { get; }
        public long EntityId { get; }
    }
}
