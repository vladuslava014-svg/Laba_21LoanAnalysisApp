using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LoanAnalysis.Domain;

namespace LoanAnalysis.Data
{
    public class XmlDataProvider
    {
        public List<LoanApplication> LoadData(string filePath)
        {
            if (!File.Exists(filePath)) return new List<LoanApplication>();
            var serializer = new XmlSerializer(typeof(List<LoanApplication>));
            using var reader = new StreamReader(filePath);
            return (List<LoanApplication>)serializer.Deserialize(reader)!;
        }

        public void SaveData(string filePath, List<LoanApplication> data)
        {
            var serializer = new XmlSerializer(typeof(List<LoanApplication>));
            using var writer = new StreamWriter(filePath);
            serializer.Serialize(writer, data);
        }
    }
}
