using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using LoanAnalysis.Domain;

namespace LoanAnalysis.Data
{
    public class XlsxReportService
    {
        public void GenerateReport(List<LoanApplication> data, string filePath)
        {
            using var workbook = new XLWorkbook();

            var wsSummary = workbook.Worksheets.Add("Summary");

            wsSummary.Cell("A1").Value = "Підсумковий аналіз кредитного портфеля";
            wsSummary.Cell("A1").Style.Font.Bold = true;
            wsSummary.Cell("A1").Style.Font.FontSize = 14;

            int total = data.Count;
            int approved = data.Count(x => x.ApprovalStatus == 1);
            int rejected = total - approved;
            double totalAmount = data.Sum(x => x.Amount);
            double avgAmount = total > 0 ? data.Average(x => x.Amount) : 0;

            wsSummary.Cell(3, 1).Value = "Показник";
            wsSummary.Cell(3, 2).Value = "Значення";
            wsSummary.Range("A3:B3").Style.Font.Bold = true;

            wsSummary.Cell(4, 1).Value = "Загальна кількість заявок";
            wsSummary.Cell(4, 2).Value = total;

            wsSummary.Cell(5, 1).Value = "Схвалено";
            wsSummary.Cell(5, 2).Value = approved;

            wsSummary.Cell(6, 1).Value = "Відхилено";
            wsSummary.Cell(6, 2).Value = rejected;

            wsSummary.Cell(7, 1).Value = "Загальна сума кредитів";
            wsSummary.Cell(7, 2).Value = totalAmount;

            wsSummary.Cell(8, 1).Value = "Середня сума кредиту";
            wsSummary.Cell(8, 2).Value = avgAmount;

            wsSummary.Cell(10, 1).Value = "Мета кредиту";
            wsSummary.Cell(10, 2).Value = "Кількість";
            wsSummary.Cell(10, 3).Value = "Середня сума";
            wsSummary.Range("A10:C10").Style.Font.Bold = true;

            var grouped = data.GroupBy(x => x.Intent);
            int rowGroup = 11;
            foreach (var group in grouped)
            {
                wsSummary.Cell(rowGroup, 1).Value = group.Key;
                wsSummary.Cell(rowGroup, 2).Value = group.Count();
                wsSummary.Cell(rowGroup, 3).Value = group.Average(x => x.Amount);
                rowGroup++;
            }

            wsSummary.Columns().AdjustToContents();

            var wsData = workbook.Worksheets.Add("Data");
            wsData.Cell(1, 1).Value = "ID";
            wsData.Cell(1, 2).Value = "Вік";
            wsData.Cell(1, 3).Value = "Дохід";
            wsData.Cell(1, 4).Value = "Мета";
            wsData.Cell(1, 5).Value = "Сума";
            wsData.Cell(1, 6).Value = "Ставка";
            wsData.Cell(1, 7).Value = "Статус";
            wsData.Range("A1:G1").Style.Font.Bold = true;

            int row = 2;
            foreach (var item in data)
            {
                wsData.Cell(row, 1).Value = item.Id;
                wsData.Cell(row, 2).Value = item.Applicant?.Age ?? 0;
                wsData.Cell(row, 3).Value = item.Applicant?.Income ?? 0;
                wsData.Cell(row, 4).Value = item.Intent;
                wsData.Cell(row, 5).Value = item.Amount;
                wsData.Cell(row, 6).Value = item.InterestRate;
                wsData.Cell(row, 7).Value = item.ApprovalStatus == 1 ? "Схвалено" : "Відхилено";

                if (item.ApprovalStatus == 1)
                    wsData.Cell(row, 7).Style.Fill.BackgroundColor = XLColor.LightGreen;
                else
                    wsData.Cell(row, 7).Style.Fill.BackgroundColor = XLColor.LightCoral;

                row++;
            }

            wsData.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }
    }
}
