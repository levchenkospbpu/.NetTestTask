using System.Text;

namespace TestTask
{
    internal class LogGenerator
    {
        public void Generate(string filePath = "Log.txt", int capacity = 1000)
        {

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                fs.SetLength(0);
                var rand = new Random();
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < capacity; i++)
                {
                    DateTime date = new DateTime(2024, 1, 1);
                    int range = (DateTime.Today - date).Days;
                    date = date.AddDays(rand.Next(range));
                    date = date.AddSeconds(rand.Next(86400));

                    sb.Append(rand.Next(200, 256).ToString() + ".");
                    sb.Append(rand.Next(255, 256).ToString() + ".");
                    sb.Append(rand.Next(255, 256).ToString() + ".");
                    sb.Append(rand.Next(255, 256).ToString());
                    sb.Append(":");
                    sb.Append(date.ToString("yyyy-MM-dd HH:mm:ss\n"));
                }

                Byte[] textBytes = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(textBytes, 0, sb.Length);
            }
        }
    }
}
