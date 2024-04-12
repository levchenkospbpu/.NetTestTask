using System.Text.RegularExpressions;

namespace TestTask
{
    internal class LogAnalyzer
    {
        public Dictionary<string, uint> AnalyzeLogs(string filePath, string ipAdressStart, decimal ipAdressMask, DateTime timeStart, DateTime timeEnd)
        {
            var res = new Dictionary<string, uint>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                var iPValidator = new IPValidator();
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var logLine = HandleLogLine(line);

                    if (logLine.Value >= timeStart && logLine.Value <= timeEnd && iPValidator.ValidateIPv4(logLine.Key, ipAdressStart, (uint)ipAdressMask))
                        if (res.TryGetValue(logLine.Key, out var count))
                            res[logLine.Key] = count + 1;
                        else
                            res.Add(logLine.Key, 1);
                }
            }

            return res;
        }

        private KeyValuePair<string, DateTime> HandleLogLine(string line)
        {
            var regex = new Regex(@"^([\d\.]+):(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})$");

            var match = regex.Match(line);
            if (!match.Success || match.Groups.Count != 3)
                throw new FormatException();

            var ipAddressStr = match.Groups[1].Value;
            var timestampStr = match.Groups[2].Value;

            if (!DateTime.TryParseExact(timestampStr, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out var timestamp))
                throw new FormatException();

            return new KeyValuePair<string, DateTime>(ipAddressStr, timestamp);
        }
    }
}