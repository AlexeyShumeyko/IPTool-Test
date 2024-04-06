using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTool
{
    internal class FileHandler
    {
        internal static List<string> ReadLogFromFile (string path)
        {
            try
            {
                return File.ReadAllLines(path).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading log file: " + ex.Message);
                return new List<string>();
            }
        }

        internal static void SaveResults (Dictionary<string, int> ipAddresses, string outputFilePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(outputFilePath))
                {
                    foreach (var result in ipAddresses)
                    {
                        sw.WriteLine($"{result.Key}:{result.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to output file: " + ex.Message);
                return;
            }

            Console.WriteLine("Success!");
        }
    }
}
