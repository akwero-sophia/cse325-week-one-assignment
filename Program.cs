using System;
using System.IO;
using System.Text;

partial class Program
{
    static void Main(string[] args)
    {
        GenerateSalesSummary("SalesData", "SalesSummary.txt");
        Console.WriteLine("Report generated!");
    }

    static void GenerateSalesSummary(string folderPath, string outputFile)
    {
        StringBuilder report = new StringBuilder();

        report.AppendLine("Sales Summary");
        report.AppendLine("----------------------------");

        decimal totalSales = 0m;

        string[] files = Directory.GetFiles(folderPath);

        foreach (var file in files)
        {
            decimal fileTotal = 0m;

            var lines = File.ReadAllLines(file);

            foreach (var line in lines)
            {
                if (decimal.TryParse(line, out decimal value))
                {
                    fileTotal += value;
                }
            }

            totalSales += fileTotal;

            report.AppendLine($"{Path.GetFileName(file)}: {fileTotal:C}");
        }

        report.Insert(report.ToString().IndexOf('\n') + 1,
            $"Total Sales: {totalSales:C}\n\nDetails:\n");

        File.WriteAllText(outputFile, report.ToString());
    }
}