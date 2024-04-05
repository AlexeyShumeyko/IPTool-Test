using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPTool
{
    public static class Startup
    {
        internal const string FILE_LOG = "--file-log";
        internal const string FILE_OUTPUT = "--file-output";
        internal const string ADDRESS_START = "--address-start";
        internal const string ADDRESS_MASK = "--address - mask";
        internal const string TIME_START = "--time-start";
        internal const string TIME_END = "--time-end";

        internal static string GetFilePathArg(string[] args, string argName)
        {
            int index = Array.IndexOf(args, argName);

            if (index > -1 && index < args.Length - 1)
            {
                return args[index + 1];
            }

            return null;
        }

        internal static DateTime GetTimeArg(string[] args, string argName) 
        {
            var time = GetFilePathArg(args, argName);

            if (time != null)
            {
                return DateTime.ParseExact(time, "dd:MM:yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return DateTime.MinValue;
        }
    }
}
