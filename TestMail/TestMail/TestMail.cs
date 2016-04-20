using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using NUnit.Framework;

namespace TestMail
{
    public class Tests
    {
        private LoginPage logIn;
        private MailPage email;
        private WebElements elements;
        private StringBuilder testLog;
        private string testMessageSubject;
        private bool emaiIsSent = false;

        public Tests()
        {
            testLog = new StringBuilder();
        }                       

        public StringBuilder TestLog
        {
            get { return testLog; }
        }

        public bool EmailIsSent
        {
            get { return emaiIsSent; }
        }

        /// <summary>
        /// Verifies that email is sent successfully
        /// </summary>
        public void SendEmail()
        {
            testLog.Append("\"send email\" test:");           
            Random rand = new Random();
            elements = new WebElements();
            if(elements.IsBrowserOpen().Equals(false))
            {
                testLog.AppendLine("Unable to open Chrome Browser. Please check your <pathDriver> value");
                testLog.AppendLine("FAILED");
                return;
            }
            if (LogIn(elements) == false)
            {                
                testLog.AppendLine("FAILED");
                return;
            }
            testMessageSubject = "Test" + rand.Next().ToString();            
            email = new MailPage(elements);
            email.ComposeMessage();            
            email.EnterReceiver(Program.settings["username"]);
            email.EnterSubject(testMessageSubject);
            email.EnterMessage("Test Message");
            email.SendMessage();                       
            email.ShowSentItems();
            try
            {                              
                Assert.IsTrue(email.GetLastSentMessage(testMessageSubject).Equals(testMessageSubject), "Message was not sent");
                testLog.AppendLine("OK");
                emaiIsSent = true;
            }
            catch(AssertionException e)
            {
                testLog.AppendLine(e.Message);
                testLog.AppendLine("FAILED");
            }
            email.LogOut();
            try
            {
                elements.QuitBrowser();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }            
        }
        /// <summary>
        /// Verifies that message was received (looks at the newest one)
        /// </summary>
        public void ReceiveEmail()
        {
            testLog.Append("\"receive email\" test: ");            
            elements = new WebElements();
            if (elements.IsBrowserOpen().Equals(false))
            {
                testLog.AppendLine("Unable to open Chrome Browser. Please check your <pathDriver> value");
                testLog.AppendLine("FAILED");
                return;
            }
            if (LogIn(elements) == false)
            {
                testLog.AppendLine("FAILED");
                return;
            }            
            email = new MailPage(elements);
            string msgSubject = testMessageSubject;
            if (Program.settings["test_to_run"] == "receive_email")
            {
                msgSubject = Program.settings["test_message"].ToString();
            }
            try
            {
                Assert.IsTrue(email.GetLastReceivedMessage().Equals(msgSubject), "Message was not received");
                testLog.AppendLine("OK");
            }
            catch(AssertionException)
            {                
                testLog.AppendLine("FAILED");
            }
            email.LogOut();
            elements.QuitBrowser();
        }
       
        private bool LogIn(WebElements elements)
        {
            bool loggedIn = false;
            logIn = new LoginPage(elements);
            if (elements.GetPageTitle().Equals("Gmail") == false)
            {
                testLog.AppendLine("Unable to reach gmail.com website. Please, check your Internet connection");
                return loggedIn;
            }
            logIn.EnterUserName(Program.settings["username"]);
            logIn.MoveForward();
            try
            {
                Assert.IsFalse(logIn.IsLoginErrorMessagePresent());
            }
            catch
            {
                testLog.AppendLine(logIn.GetLoginErrorMessage());
                elements.QuitBrowser();
                return loggedIn;
            }            
            logIn.EnterPassword(Program.settings["password"]);
            logIn.LogIn();
            try
            {
                Assert.IsFalse(logIn.IsPasswordErrorMessagePresent());               
            }
            catch
            {
                testLog.AppendLine(logIn.GetPasswordErrorMessage());
                elements.QuitBrowser();
                return loggedIn;
            }
            loggedIn = true;
            return loggedIn;     
        }
    }
}
