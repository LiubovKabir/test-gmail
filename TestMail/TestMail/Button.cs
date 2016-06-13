using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TestMail
{
    public class Button: WebElement
    {        

        public Button(Browser browser, By selector): base (browser,selector)
        {  
        }

        public void ClickOnButton()
        {
            IWebElement Btn = WebBrowser.Driver.FindElement(Selector);
            if (Btn.Displayed)
                Btn.Click();
            WebBrowser.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }
    }
}
