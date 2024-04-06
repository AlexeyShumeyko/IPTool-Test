using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace IPTool
{
    internal class IPFilter
    {
        internal static Dictionary<string, int> FilterLogData(
            List<string> logLines, 
            string addressStart, 
            string addressMask,
            DateTime timeStart, 
            DateTime timeEnd)

        {
            Dictionary<string, int> ipAddresses = new Dictionary<string, int>();

            foreach (var line in logLines)
            {
                string[] parts = line.Split(' ');
                parts = parts[0].Split(":");

                string timeStamp = parts[1];

                if (IsTimeInRange(timeStamp, timeStart, timeEnd))
                {
                    string ipAddress = parts[0];

                    if (IsIPAddressInRange(ipAddress, addressStart, addressMask))
                    {
                        if (ipAddresses.ContainsKey(ipAddress))
                        {
                            ipAddresses[ipAddress]++;
                        }
                        else
                        {
                            ipAddresses[ipAddress] = 1;
                        }
                    }
                }
            }

            return ipAddresses;
        }

        private static bool IsIPAddressInRange(string ipAddress, string addressStart, string addressMask) 
        {
            if (addressStart != null)
            {
                var ipParts = long.Parse(string.Join("", ipAddress.Split('.').Select(x => $"00{x}".Substring($"00{x}".Length - 3))));
                var startParts = long.Parse(string.Join("", addressStart.Split('.').Select(x => $"00{x}".Substring($"00{x}".Length - 3))));

                if (ipParts <= startParts)
                {
                    return false;
                }
            }

            if (addressMask != null)
            {
                var ipParts = ipAddress.Split(".");
                var startParts = addressStart.Split('.');

                uint ipNumber = Convert.ToUInt32(ipParts[3]);
                uint startNumber = Convert.ToUInt32(startParts[3]);
                uint addressMaskNumber = Convert.ToUInt32(addressMask);
                uint maskNumber = (uint)Math.Pow(2, 32 - addressMaskNumber) - 1;

                uint rangeNumber = startNumber & maskNumber;

                if ((ipNumber & maskNumber) != rangeNumber)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsTimeInRange(string timeStamp, DateTime timeStart, DateTime timeEnd)
        {
            DateTime timeValue = DateTime.Parse(timeStamp);

            return timeValue >= timeStart && timeValue <= timeEnd;
        }
    }
}
