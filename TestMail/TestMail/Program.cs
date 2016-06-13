using System;
using System.Configuration;
using System.Collections.Generic;

namespace TestMail
{
    class Program
    {

        public static Dictionary<string, string> Settings;
        public const string LOG_PATH = "test-log.txt";
        public const string SEND_EMAIL_TEST = "send_email";
        public const string RECEIVE_EMAIL_TEST = "receive_email";


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
            if (Settings.Count == 0)
            {
                return;
            }
            Browser browser = new Browser();    //start new Browser session
            if (browser.Driver != null)
            {
                Tests testrun = new Tests(browser);
                //Logging added
                Logger.WriteLine(new[] { "Test Run Summary:", DateTime.Now.ToString() });                
                try
                {
                    switch (Settings["test_to_run"].ToLower())
                    {
                        case SEND_EMAIL_TEST:
                            testrun.SendEmail();
                            break;
                        case RECEIVE_EMAIL_TEST:
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
                    Logger.WriteLine(new[] { "---- DONE ----" });
                }
                finally
                {
                    browser.Quit();
                }                
            }
        }

        static Dictionary<string, string> ReadAppSettings(string filePath)
        {
            // Get the settings collection (key/value pairs).
            Settings = new Dictionary<string, string>();
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
                    Settings.Add(key, value);
                }
            }
            else
                Console.WriteLine("The appSettings section is empty. Please check your configuration file.");
            return Settings;
        }
    }
}
