using DL.Infrastructure;

namespace DL.ECS.Team.Scenarios
{
    internal static class SystemNotificationExtensions
    {
        internal static void SystemSetup(this ISystemNotification systemNotification)
            => systemNotification.Logger.Information($"System setup");

        internal static void FixtureSystem(this ISystemNotification systemNotification)
            => systemNotification.Logger.Information($"Fixture system");

        internal static void MatchSystem(this ISystemNotification systemNotification)
            => systemNotification.Logger.Information($"Match system");

        internal static void ResultSystem(this ISystemNotification systemNotification)
            => systemNotification.Logger.Information($"Result system");
    }
}
