using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using LoanAnalysis.Domain;

namespace LoanAnalysis.Data
{
    public class CsvDataProvider
    {
        public List<LoanApplication> LoadData(string filePath)
        {
            if (!File.Exists(filePath)) return new List<LoanApplication>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<LoanApplicationMap>();
            return csv.GetRecords<LoanApplication>().ToList();
        }

        public void SaveData(string filePath, List<LoanApplication> data)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);

            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, config);
            csv.Context.RegisterClassMap<LoanApplicationMap>();
            csv.WriteRecords(data);
        }
    }
}