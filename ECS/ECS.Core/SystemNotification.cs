using DL.Infrastructure;

namespace DL.ECS.Core
{
    internal static class SystemNotificationExtensions
    {
        internal static void EntityCreated(this ISystemNotification systemNotification,
            IEntity entity)
            => systemNotification.Logger.Information($"Entity created: {entity}");
    }
}
