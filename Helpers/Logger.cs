using System;
using System.IO;

namespace FormTests.Helpers
{
    public static class Logger
    {
        private static readonly string LogFile = "test_log.txt";

        public static void Log(string message, string level = "INFO")
        {
            string logMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
            
            ConsoleColor color = level switch
            {
                "SUCCESS" => ConsoleColor.Green,
                "ERROR" => ConsoleColor.Red,
                "WARNING" => ConsoleColor.Yellow,
                _ => ConsoleColor.Gray
            };
            
            Console.ForegroundColor = color;
            Console.WriteLine(logMessage);
            Console.ResetColor();
            
            File.AppendAllText(LogFile, logMessage + Environment.NewLine);
        }
    }
}