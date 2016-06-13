using System;
using System.Collections.Generic;
using System.IO;

namespace TestMail
{
    static public class Logger
    {
        public static void WriteLine(IEnumerable<string> contents)
        {
            bool result;
            if (Boolean.TryParse(Program.Settings["logging"], out result))
            {
                foreach (string msg in contents)
                {
                    Console.WriteLine(msg);
                }
                if (Convert.ToBoolean(Program.Settings["logging"]))
                {
                    File.AppendAllLines(Program.LOG_PATH, contents);
                }
            }
            else
            {
                foreach (string msg in contents)
                {
                    Console.WriteLine(msg);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Warning]: Logging to the log file is not available. Please check your configuration file settings.Setting 'logging' must be True or False");
                Console.ResetColor();
            }
        }

        public static void DeleteOldLogFile()
        {
            File.Delete(Program.LOG_PATH);
        }
    }
}
