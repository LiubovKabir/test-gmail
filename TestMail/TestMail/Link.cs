using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TestMail
{
    public class Link: WebElement
    {

        public Link(Browser browser, By selector) : base(browser, selector)
        {           
        }

        public void ClickOnLink()
        {
            WebBrowser.Driver.FindElement(Selector).Click();
        }
    }
}
