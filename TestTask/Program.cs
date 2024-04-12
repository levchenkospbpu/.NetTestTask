using System.Text;
using TestTask;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileLog = string.Empty;
            var fileOutput = string.Empty;
            var ipAdressStart = "0.0.0.0";
            var ipAdressMask = uint.MaxValue;
            var timeStart = DateTime.MinValue;
            var timeEnd = DateTime.MaxValue;

            var adressStartParamGot = false;
            var adressMaskParamGot = false;

            try
            {
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
                            adressStartParamGot = true;
                            break;
                        case "--address-mask":
                            ipAdressMask = Convert.ToUInt32(args[i + 1]);
                            adressMaskParamGot = true;
                            break;
                        case "--time-start":
                            if (!DateTime.TryParseExact(args[i + 1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out timeStart))
                            {
                                Console.WriteLine($"Invalid timestamp format: {args[i + 1]}");
                                return;
                            }
                            break;
                        case "--time-end":
                            if (!DateTime.TryParseExact(args[i + 1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out timeEnd))
                            {
                                Console.WriteLine($"Invalid timestamp format: {args[i + 1]}");
                                return;
                            }
                            break;
                        default:
                            Console.WriteLine($"Unknown parameter: {args[i]}");
                            return;
                    }
                }

                if (adressMaskParamGot && !adressStartParamGot)
                {
                    Console.WriteLine($"Сan't use --address-mask without --address-start");
                    return;
                }

                LogAnalyzer logAnalyzer = new LogAnalyzer();
                var ipCounts = logAnalyzer.AnalyzeLogs(fileLog, ipAdressStart, ipAdressMask, timeStart, timeEnd);

                WriteResults(fileOutput, ipCounts);
            }

            catch (FormatException)
            {
                Console.WriteLine("Invalid parametr format");
            }

            catch (OverflowException)
            {
                Console.WriteLine("Invalid parametr value");
            }

            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("The required permission is missing");
            }

            catch (ArgumentNullException)
            {
                Console.WriteLine("File path not specified");
            }

            catch (ArgumentException)
            {
                Console.WriteLine("File path is invalid");
            }

            catch (PathTooLongException)
            {
                Console.WriteLine("The specified path, file name, or both exceeds the maximum length");
            }

            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Invalid path specified");
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }

            catch (NotSupportedException)
            {
                Console.WriteLine("The file path is in an invalid format");
            }
        }

        static void WriteResults(string fileOutput, Dictionary<string, uint> ipCounts)
        {
            using (FileStream fs = new FileStream(fileOutput, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
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