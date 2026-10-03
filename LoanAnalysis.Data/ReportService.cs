using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using LoanAnalysis.Domain;

namespace LoanAnalysis.Data
{
    public class ReportService
    {
        public void ExportToJson(List<LoanApplication> data, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        public void ExportToXml(List<LoanApplication> data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<LoanApplication>));
            using var stream = new StreamWriter(filePath, false, Encoding.UTF8);
            serializer.Serialize(stream, data);
        }

        public void GenerateExcelReport(List<LoanApplication> data, string filePath)
        {
            var sb = new StringBuilder();

            sb.AppendLine("ID;Вік;Дохід;Мета;Сума;Ставка;Статус");

            foreach (var item in data)
            {
                int age = item.Applicant?.Age ?? 0;
                double income = Convert.ToDouble(item.Applicant?.Income ?? 0);
                double amount = Convert.ToDouble(item.Amount);
                double rate = Convert.ToDouble(item.InterestRate);
                string status = item.ApprovalStatus == 1 ? "Схвалено" : "Відхилено";

                sb.AppendLine($"{item.Id};{age};{income};{item.Intent};{amount};{rate};{status}");
            }

            sb.AppendLine();
            sb.AppendLine($"Всього записів;{data.Count}");
            double avgAmount = data.Any() ? data.Average(x => Convert.ToDouble(x.Amount)) : 0;
            sb.AppendLine($"Середня сума;{avgAmount:F2}");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        public void GenerateWordReport(List<LoanApplication> data, string filePath)
        {
            var sb = new StringBuilder();

            int total = data.Count;
            int approved = data.Count(x => x.ApprovalStatus == 1);
            int rejected = total - approved;
            double avgAmount = total > 0 ? data.Average(x => Convert.ToDouble(x.Amount)) : 0;

            sb.AppendLine("==================================================");
            sb.AppendLine("        ЗВІТ З АНАЛІЗУ КРЕДИТНИХ ЗАЯВОК           ");
            sb.AppendLine("==================================================");
            sb.AppendLine($"Дата формування: {DateTime.Now:dd.MM.yyyy HH:mm}");
            sb.AppendLine();
            sb.AppendLine($"Загальна кількість заявок: {total}");
            sb.AppendLine($"Схвалено: {approved} ({(total > 0 ? (approved * 100.0 / total) : 0):F1}%)");
            sb.AppendLine($"Відхилено: {rejected}");
            sb.AppendLine($"Середня сума кредиту: {avgAmount:F2} грн");
            sb.AppendLine("==================================================");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
    }
}