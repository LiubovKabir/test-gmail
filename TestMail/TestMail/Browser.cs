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
            driver = StartBrowser(Program.settings["browser"], Program.settings["url"], Program.settings["driverPath"]);
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
                    Console.WriteLine("browser : " + browser + " is invalid, Launching Firefox as default browser");
                    webdriver = StartFirefox(url);
                    break;
            }
            if (webdriver != null)
            {
                try
                {
                    webdriver.Navigate().GoToUrl(url);
                    Assert.AreEqual("Gmail", webdriver.Title, "Unable to reach gmail.com website");
                }
                catch (AssertionException)
                { }
                webdriver.Manage().Window.Maximize();
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
