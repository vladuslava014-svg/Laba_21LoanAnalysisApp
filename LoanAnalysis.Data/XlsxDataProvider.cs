using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using LoanAnalysis.Domain;

namespace LoanAnalysis.Data
{
    public class XlsxDataProvider
    {
        public List<LoanApplication> LoadData(string filePath)
        {
            var list = new List<LoanApplication>();
            if (!File.Exists(filePath)) return list;

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null) return list;

            var rows = worksheet.RowsUsed().Skip(1); 
            int idCounter = 1;

            foreach (var row in rows)
            {
                var app = new LoanApplication
                {
                    Id = idCounter++,
                    Applicant = new Applicant
                    {
                        Age = row.Cell(1).GetValue<int>(),
                        Income = row.Cell(2).GetValue<double>(),
                        HomeOwnership = row.Cell(3).GetString(),
                        EmploymentLength = row.Cell(4).GetValue<double>()
                    },
                    Intent = row.Cell(5).GetString(),
                    Amount = row.Cell(6).GetValue<double>(),
                    InterestRate = row.Cell(7).GetValue<double>(),
                    ApprovalStatus = row.Cell(8).GetString() == "Схвалено" ? 1 : 0
                };
                list.Add(app);
            }
            return list;
        }

        public void SaveData(string filePath, List<LoanApplication> data)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Loans");

            ws.Cell(1, 1).Value = "Вік";
            ws.Cell(1, 2).Value = "Дохід";
            ws.Cell(1, 3).Value = "Житло";
            ws.Cell(1, 4).Value = "Стаж";
            ws.Cell(1, 5).Value = "Мета";
            ws.Cell(1, 6).Value = "Сума";
            ws.Cell(1, 7).Value = "Ставка";
            ws.Cell(1, 8).Value = "Статус";

            int row = 2;
            foreach (var item in data)
            {
                ws.Cell(row, 1).Value = item.Applicant?.Age ?? 0;
                ws.Cell(row, 2).Value = item.Applicant?.Income ?? 0;
                ws.Cell(row, 3).Value = item.Applicant?.HomeOwnership ?? "";
                ws.Cell(row, 4).Value = item.Applicant?.EmploymentLength ?? 0;
                ws.Cell(row, 5).Value = item.Intent;
                ws.Cell(row, 6).Value = item.Amount;
                ws.Cell(row, 7).Value = item.InterestRate;
                ws.Cell(row, 8).Value = item.ApprovalStatus == 1 ? "Схвалено" : "Відхилено";
                row++;
            }

            ws.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }
    }
}
