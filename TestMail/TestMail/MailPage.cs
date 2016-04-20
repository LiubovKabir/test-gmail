using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TestMail
{
    class MailPage
    {
        private WebElements wbElements;

        private By composeButton = By.CssSelector("div[class='T-I J-J5-Ji T-I-KE L3']");
        private By receiverTextBox = By.Name("to");
        private By subjectTextBox = By.Name("subjectbox");
        private By bodyTextBox = By.CssSelector("div[class='Am Al editable LW-avf']");
        private By sendButton = By.CssSelector("div[class='T-I J-J5-Ji aoO T-I-atl L3']");
        private By logoutButton = By.Id("gb_71");
        private By accountButton = By.CssSelector("span.gb_2a.gbii");
        private By sentItemsRef = By.XPath(@"//div[4]/div/div/div/span/a");
        private By lastReceivedMessage = By.XPath("//body//table//tr[1]/td[6]//span[1]");
        private By lastSentMessage = By.XPath(@"(//div[@class='nH nn']//table/tbody[1]/tr[1]/td[6]//span[1])[last()]");

        public MailPage(WebElements wbElements)
        {
            this.wbElements = wbElements;
        }

        public void EnterReceiver(string receiver)
        {
            wbElements.EnterText(receiverTextBox, receiver);
        }

        public void EnterMessage(string message)
        {            
            wbElements.EnterText(bodyTextBox, message);
        }

        public void EnterSubject(string subject)
        {
            wbElements.EnterText(subjectTextBox, subject);
        }

        public void ComposeMessage()
        {
            wbElements.ClickOnButton(composeButton);
        }

        public void SendMessage()
        {
            wbElements.ClickOnButton(sendButton);            
        }

        public void LogOut()
        {
            wbElements.ClickOnButton(accountButton);
            wbElements.ClickOnButton(logoutButton);
        }        

        public void ShowSentItems() 
        {
            wbElements.ClickOnReference(sentItemsRef);
            System.Threading.Thread.Sleep(6000);
        }  
        
        public string GetLastReceivedMessage()
        {
            return wbElements.GetElementText(lastReceivedMessage);
        }

        public string GetLastSentMessage(string msgsubject)
        {
            return wbElements.GetElementText(lastSentMessage);
        }

        public string GetErrorMessage()
        {
            return wbElements.GetElementText(lastSentMessage);
        }
    }
}
