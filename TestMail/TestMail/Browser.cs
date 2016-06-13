using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace TestMail
{
    public class Browser
    {
        private IWebDriver driver;       

        public Browser()
        {            
            driver = StartBrowser(Program.Settings["browser"], Program.Settings["url"], Program.Settings["driverPath"]);
        }

        public IWebDriver Driver
        {
            get { return driver; }
        }
        
        public void Quit()
        {
            if (driver == null) return;
            driver.Quit();
            driver = null;
        }

        /// <summary>
        /// Starts the selected browser and open the specified url
        /// </summary>
        private IWebDriver StartBrowser(string browser, string url, string driverPath)
        {
            IWebDriver webdriver;
            Logger.DeleteOldLogFile();          
            switch (browser)
            {
                case "Firefox":
                    webdriver = StartFirefox(url);
                    break;
                case "Chrome":
                    try
                    {
                        webdriver = new ChromeDriver(driverPath);
                    }
                    catch
                    {
                        webdriver = null;
                    }
                    break;
                default:                              
                    //Logging
                    string appendText = "browser : " + browser + " is invalid, Launching Firefox as default browser";
                    Logger.WriteLine(new[] { appendText });
                    webdriver = StartFirefox(url);
                    break;
            }
            if (webdriver != null)
            {
                try
                {
                    webdriver.Navigate().GoToUrl(url);
                    Assert.AreEqual("Gmail", webdriver.Title);
                }
                catch (AssertionException)
                {
                    //Logging
                    Logger.WriteLine(new[] { "Unable to reach gmail.com website" });
                }
                webdriver.Manage().Window.Maximize();
            } 
            else
            {
                //Logging
                string appendText = "Unable to open Chrome Browser. Please check your configuration file";
                Logger.WriteLine(new[] { appendText });
            }
                       
            return webdriver;
        }

        private IWebDriver StartFirefox(string url)
        {
            var firefoxProfile = new FirefoxProfile
            {
                AcceptUntrustedCertificates = true,
                EnableNativeEvents = true
            };

            IWebDriver webdriver = new FirefoxDriver(firefoxProfile);            
            return webdriver;
        }
    }
}
