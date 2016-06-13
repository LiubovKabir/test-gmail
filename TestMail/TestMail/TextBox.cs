
using OpenQA.Selenium;

namespace TestMail
{
    public class TextBox: WebElement
    {
        private string text;                       

        public TextBox(Browser browser, By selector, string text) : base(browser, selector)
        {
            this.text = text;
        }

        public void EnterText()
        {
            IWebElement TxtBox = WebBrowser.Driver.FindElement(Selector);
            if (TxtBox.Displayed)
                TxtBox.Clear();
            TxtBox.SendKeys(text);
        }
    }
}
