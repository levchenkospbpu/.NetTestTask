using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class AnalyzerOptions
    {
        public string FileLog { get; set; }
        public string FileOutput { get; set; }
        public string IpAdressStart { get; set; } = string.Empty;
        public decimal IpAdressMask { get; set; } = decimal.MaxValue;
        public DateTime TimeStart { get; set; } = DateTime.MinValue;
        public DateTime TimeEnd { get; set; } = DateTime.MaxValue;

        public AnalyzerOptions(string fileLog, string fileOutput, string ipAdressStart, decimal ipAdressMask, DateTime timeStart, DateTime timeEnd)
        {
            FileLog = fileLog;
            FileOutput = fileOutput;
            IpAdressStart = ipAdressStart;
            IpAdressMask = ipAdressMask;
            TimeStart = timeStart;
            TimeEnd = timeEnd;
        }
    }
}
