using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace TestMail
{
    class Program
    {

        public static Dictionary<string, string> settings;


        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Please, specify a configuration file:");
                return;
            }
            try
            {
                ReadAppSettings(args[0]);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return;
            }
            if (settings.Count == 0)
            {
                return;
            }
            Tests testrun = new Tests();
            testrun.TestLog.AppendLine("Test Run Summary:");
            testrun.TestLog.AppendLine(DateTime.Now.ToString());
            switch (settings["test_to_run"])
            {
                case "send_email":
                    testrun.SendEmail();
                    break;
                case "receive_email":
                    testrun.ReceiveEmail();
                    break;
                default:
                    testrun.SendEmail();
                    if (testrun.EmailIsSent)
                    {
                        testrun.ReceiveEmail();
                    }
                    break;
            }
            Console.WriteLine("Test Run finished");
            if (settings["logging"].Equals("on"))
            {
                File.WriteAllText("test-log.txt", testrun.TestLog.ToString());
            }
        }

        static Dictionary<string, string> ReadAppSettings(string filePath)
        {
            // Get the settings collection (key/value pairs).
            settings = new Dictionary<string, string>();
            AppSettingsSection appSettings;
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = filePath;
            // Get the configuration file
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            // Get the appSettings section.
            appSettings = (AppSettingsSection)config.GetSection("appSettings");
            if (appSettings.Settings.Count != 0)
            {
                foreach (string key in appSettings.Settings.AllKeys)
                {
                    string value = appSettings.Settings[key].Value;
                    settings.Add(key, value);
                }
            }
            else
                Console.WriteLine("The appSettings section is empty. Please check your configuration file.");
            return settings;
        }
    }
}
