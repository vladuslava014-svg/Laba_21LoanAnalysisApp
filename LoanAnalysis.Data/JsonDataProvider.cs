using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LoanAnalysis.Domain;

namespace LoanAnalysis.Data
{
    public class JsonDataProvider
    {
        public List<LoanApplication> LoadData(string filePath)
        {
            if (!File.Exists(filePath)) return new List<LoanApplication>();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<LoanApplication>>(json) ?? new List<LoanApplication>();
        }

        public void SaveData(string filePath, List<LoanApplication> data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }
    }
}
