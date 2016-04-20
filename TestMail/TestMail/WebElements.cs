using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestMail
{
    public class WebElements
    {
        private Browser browser;

        public WebElements()
        {
            browser = new Browser();
        }

        public bool IsBrowserOpen()
        {
            if (browser.Driver == null)
            {
                return false;
            }
            else
            { return true; }
        }

        public void EnterText(By input, string text)
        {
            IWebElement TxtBox = browser.Driver.FindElement(input);
            if (TxtBox.Displayed)
                TxtBox.Clear();
            TxtBox.SendKeys(text);
        }

        public void ClickOnButton(By button)
        {
            IWebElement Btn = browser.Driver.FindElement(button);
            if (Btn.Displayed)
                Btn.Click();
            browser.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        public void ClickOnReference(By reference)
        {
            browser.Driver.FindElement(reference).Click();
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                browser.Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsElementDiplayed(By by)
        {
            bool displayed = false;
            if (IsElementPresent(by))
            {
                IWebElement element = browser.Driver.FindElement(by);
                if (element.Displayed)
                { displayed = true; }
            }
            return displayed;
        }

        public String GetElementText(By by)
        {
            return browser.Driver.FindElement(by).Text;
        }

        public void QuitBrowser()
        {
            try
            {
                browser.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public string GetPageTitle()
        {
            return browser.Driver.Title;
        }
    }
}
