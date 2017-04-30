using Serilog;

namespace DL.Infrastructure
{
    public interface ISystemNotification
    {
        ILogger Logger { get; }
    }
    internal class SystemNotification : ISystemNotification
    {
        public SystemNotification()
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();
            Logger = Log.Logger;
        }

        public ILogger Logger { get; }
    }
}
