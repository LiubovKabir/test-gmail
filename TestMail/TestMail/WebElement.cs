using OpenQA.Selenium;

namespace TestMail
{
    public class WebElement
    {
        private By selector;
        private Browser webbrowser;


        public By Selector
        {
            get { return selector; }
            set { selector = value; }
        }

        public Browser WebBrowser
        {
            get { return webbrowser; }
            set { webbrowser = value; }
        }

        public WebElement(Browser browser, By selector)
        {
            WebBrowser = browser;
            Selector = selector;
        }        

        public bool IsElementPresent()
        {
            try
            {
                WebBrowser.Driver.FindElement(Selector);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsElementDiplayed()
        {
            bool displayed = false;
            if (IsElementPresent())
            {
                IWebElement element = WebBrowser.Driver.FindElement(Selector);
                if (element.Displayed)
                { displayed = true; }
            }
            return displayed;
        }

        public string GetElementText()
        {
            return WebBrowser.Driver.FindElement(Selector).Text;
        }
    }
}
