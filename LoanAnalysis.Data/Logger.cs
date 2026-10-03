using System;
using System.IO;

namespace LoanAnalysis.Data
{
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app_log.txt");

        public static void Log(string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(LogFilePath, logEntry);
            }
            catch
            {
            }
        }

        public static void LogError(string message, Exception ex)
        {
            Log($"[ERROR] {message}. Деталі: {ex.Message}");
        }
    }
}
