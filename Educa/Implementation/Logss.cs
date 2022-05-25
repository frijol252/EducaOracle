using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class Logss
    {
        public static TraceListener LogError = new TextWriterTraceListener(@"D:\LogError.log");
        public static TraceListener LogInternalActivities = new TextWriterTraceListener(@"D:\LogInternalActivities.log");
    }
}
