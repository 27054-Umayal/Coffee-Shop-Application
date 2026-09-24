using System.Text.Json;
using CoffeeShopApplication.Core.Interfaces;

namespace CoffeeShopApplication.Repository
{
    public static class FileOperation<T>
    {
        public static void WriteToFile(List<T> records, string filePath)
        {
            List<string> lines = new List<string>();
            foreach (T record in records)
            {
                string line = JsonSerializer.Serialize(record);
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }

        public static void AppendToFile(T record, string filePath)
        {
            string line = JsonSerializer.Serialize(record);
            File.AppendAllText(filePath, line + Environment.NewLine);
        }

        public static List<T> ReadFromFile(string filePath)
        {
            List<T> records = new List<T>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                T? record = JsonSerializer.Deserialize<T?>(line);
                if (record is null)
                {
                    continue;
                }

                records.Add(record);
            }

            return records;
        }
    }
}
