using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LoanAnalysis.Domain;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace LoanAnalysis.Data
{
    public class DocxReportService
    {
        public void GenerateReport(List<LoanApplication> data, string filePath, string chartImagePath = null)
        {
            using var doc = DocX.Create(filePath);

            var pTitle = doc.InsertParagraph("ЗВІТ З АНАЛІЗУ ДАНИХ КРЕДИТНИХ ЗАЯВОК");
            pTitle.FontSize(20).Bold().Alignment = Alignment.center;

            var pSub = doc.InsertParagraph("Лабораторна робота №2\n");
            pSub.FontSize(14).Italic().Alignment = Alignment.center;

            doc.InsertParagraph($"Дата формування: {DateTime.Now:dd.MM.yyyy HH:mm}\n");
            doc.InsertParagraph("--------------------------------------------------------------------------------");

            var pSection1 = doc.InsertParagraph("1. Опис датасету");
            pSection1.FontSize(14).Bold();
            doc.InsertParagraph($"Загальна кількість завантажених записів: {data.Count}");
            doc.InsertParagraph("Джерело даних: CSV-датасет аналізу кредитного ризику.");

            var pSection2 = doc.InsertParagraph("\n2. Ключові метрики");
            pSection2.FontSize(14).Bold();

            int total = data.Count;
            int approved = data.Count(x => x.ApprovalStatus == 1);
            int rejected = total - approved;
            double avgAmount = total > 0 ? data.Average(x => x.Amount) : 0;

            var table = doc.AddTable(5, 2);
            table.Design = TableDesign.TableGrid;

            table.Rows[0].Cells[0].Paragraphs[0].Append("Показник").Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Значення").Bold();

            table.Rows[1].Cells[0].Paragraphs[0].Append("Всього заявок");
            table.Rows[1].Cells[1].Paragraphs[0].Append(total.ToString());

            table.Rows[2].Cells[0].Paragraphs[0].Append("Схвалено");
            table.Rows[2].Cells[1].Paragraphs[0].Append($"{approved} ({(total > 0 ? (approved * 100.0 / total) : 0):F1}%)");

            table.Rows[3].Cells[0].Paragraphs[0].Append("Відхилено");
            table.Rows[3].Cells[1].Paragraphs[0].Append(rejected.ToString());

            table.Rows[4].Cells[0].Paragraphs[0].Append("Середня сума кредиту");
            table.Rows[4].Cells[1].Paragraphs[0].Append($"{avgAmount:F2} грн");

            doc.InsertTable(table);

            if (!string.IsNullOrEmpty(chartImagePath) && File.Exists(chartImagePath))
            {
                var pSection3 = doc.InsertParagraph("\n3. Візуалізація даних");
                pSection3.FontSize(14).Bold();

                var image = doc.AddImage(chartImagePath);
                var picture = image.CreatePicture(300, 500); 
                doc.InsertParagraph().AppendPicture(picture).Alignment = Alignment.center;
            }

            var pSection4 = doc.InsertParagraph("\n4. Висновки");
            pSection4.FontSize(14).Bold();
            doc.InsertParagraph("В ході аналізу було опрацьовано датасет кредитного портфеля. " +
                               "Отримані дані дозволяють оцінити частку схвалених заявок та середню суму кредитування.");

            doc.Save();
        }
    }
}
