using DL.Infrastructure;

namespace DL.ECS.Core
{
    public static class SystemNotificationExtensions
    {
        public static void EntityCreated(this ISystemNotification systemNotification,
            IEntity entity)
            => systemNotification.Logger.Information($"Entity created: {entity}");
    }
}
