using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Infrastructure
{
    public static class AmbientLogger
    {
        public static void Setup()
        {
            SystemNotification = new SystemNotification();
        }

        public static ISystemNotification SystemNotification { get; private set; }
    }
}
