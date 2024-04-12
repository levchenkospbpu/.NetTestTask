using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TestTask;
using static System.Net.Mime.MediaTypeNames;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileLog = "Log.txt";
            var fileOutput = "Result.txt";
            var timeStart = DateTime.Now.AddMonths(-1);
            var timeEnd = DateTime.Now;
            var ipAdressStart = "150.13.13.13";
            decimal ipAdressMask = 4294967295;


            for (int i = 0; i < args.Length; i += 2)
            {
                switch (args[i])
                {
                    case "--file-log":
                        fileLog = args[i + 1];
                        break;
                    case "--file-output":
                        fileOutput = args[i + 1];
                        break;
                    case "--address-start":
                        ipAdressStart = args[i + 1];
                        break;
                    case "--address-mask":
                        ipAdressMask = Convert.ToDecimal(args[i + 1]);
                        break;
                    case "--time-start":
                        if (!DateTime.TryParseExact(args[i + 1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out timeStart))
                        {
                            throw new FormatException($"Invalid timestamp format: {args[i + 1]}");
                        }
                        break;
                    case "--time-end":
                        if (!DateTime.TryParseExact(args[i + 1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out timeEnd))
                        {
                            throw new FormatException($"Invalid timestamp format: {args[i + 1]}");
                        }
                        break;
                    default:
                        Console.WriteLine($"Unknown parameter: {args[i]}");
                        break;
                }
            }

            LogAnalyzer logAnalyzer = new LogAnalyzer();
            var ipCounts = logAnalyzer.AnalyzeLogs(fileLog, ipAdressStart, ipAdressMask, timeStart, timeEnd);

            WriteResults(fileOutput, ipCounts);
        }

        static void WriteResults(string fileOutput, Dictionary<string, uint> ipCounts)
        {
            using (FileStream fs = new FileStream("Result.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                fs.SetLength(0);
                foreach (KeyValuePair<string, uint> ipCount in ipCounts)
                {
                    var resStr = $"{ipCount.Key}: {ipCount.Value}\n";
                    Byte[] textBytes = new UTF8Encoding(true).GetBytes(resStr);
                    fs.Write(textBytes, 0, resStr.Length);
                }
            }
        }
    }
}