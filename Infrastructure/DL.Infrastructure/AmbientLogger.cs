namespace DL.Infrastructure
{
    public static class AmbientLogger
    {
        private static ISystemNotification _systemNotification;
        public static ISystemNotification SystemNotification
        {
            get
            {
                if(_systemNotification == null)
                    _systemNotification = new SystemNotification();
                return _systemNotification;
            }
        }
    }
}
