
using OpenQA.Selenium;

namespace TestMail
{
    class MailPage
    {
        private Browser webbrowser;

        private TextBox receiverTextBox, subjectTextBox, bodyTextBox;
        private Button composeButton, sendButton, logoutButton, accountButton;
        private Link sentItemLink;
        private WebElement lastReceivedMessage, lastSentMessage;

        public MailPage(Browser browser,string receiver, string message, string subject)
        {
            webbrowser = browser;
            receiverTextBox = new TextBox(webbrowser, By.Name("to"), receiver);
            subjectTextBox = new TextBox(webbrowser, By.Name("subjectbox"), subject);
            bodyTextBox = new TextBox(webbrowser, By.CssSelector("div[class='Am Al editable LW-avf']"), message);
            composeButton = new Button(webbrowser, By.CssSelector("div[class='T-I J-J5-Ji T-I-KE L3']"));
            sendButton = new Button(webbrowser, By.CssSelector("div[class='T-I J-J5-Ji aoO T-I-atl L3']"));
            logoutButton = new Button(webbrowser, By.XPath("//a[contains(@href, 'Logout')]"));
            accountButton = new Button(webbrowser, By.XPath(@"//a[contains(@href, 'SignOutOptions')]"));
            sentItemLink = new Link(webbrowser, By.XPath(@"//div[4]/div/div/div/span/a"));
            lastReceivedMessage = new WebElement(webbrowser, By.XPath("//body//table//tr[1]/td[6]//span[1]"));
            lastSentMessage = new WebElement(webbrowser, By.XPath(@"(//div[@class='nH nn']//table/tbody[1]/tr[1]/td[6]//span[1])[last()]"));

        }

        public void EnterReceiver()
        {
            receiverTextBox.EnterText();
            return;
        }

        public void EnterMessage()
        {            
            bodyTextBox.EnterText();
            return;
        }

        public void EnterSubject()
        {
            subjectTextBox.EnterText();
            return;
        }

        public void ComposeMessage()
        {
            composeButton.ClickOnButton();
            return;
        }

        public void SendMessage()
        {
            sendButton.ClickOnButton();
            return;          
        }

        public void LogOut()
        {
            accountButton.ClickOnButton();
            logoutButton.ClickOnButton();
            return;
        }        

        public void ShowSentItems() 
        {
            sentItemLink.ClickOnLink();
            System.Threading.Thread.Sleep(6000);
            return;
        }  
        
        public string GetLastReceivedMessage()
        {
            return lastReceivedMessage.GetElementText();
        }

        public string GetLastSentMessage()
        {
            return lastSentMessage.GetElementText();
        }       
    }
}
