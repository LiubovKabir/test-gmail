using System;
using NUnit.Framework;

namespace TestMail
{
    public class Tests
    {
        private Browser browser;
        private LoginPage logIn;
        private MailPage email;

        private string testMessageSubject;
        private bool emaiIsSent = false;


        public Tests(Browser webbrowser)
        {
            browser = webbrowser;
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
            Logger.WriteLine(new[] { "\"send email\" test:" });
            Random rand = new Random();
            if (LogIn() == false)
            {
                Logger.WriteLine(new[] { "FAILED" });
                return;
            }
            testMessageSubject = "Test" + rand.Next().ToString();
            email = new MailPage(browser, Program.Settings["username"], "Test Message", testMessageSubject);
            email.ComposeMessage();
            email.EnterReceiver();
            email.EnterSubject();
            email.EnterMessage();
            email.SendMessage();
            email.ShowSentItems();
            try
            {
                Assert.IsTrue(email.GetLastSentMessage().Equals(testMessageSubject));
                Logger.WriteLine(new[] { "OK" });
                emaiIsSent = true;
            }
            catch (AssertionException)
            {
                Logger.WriteLine(new[] { "Message was not sent", "FAILED" });
            }
            finally
            {
                email.LogOut();
            }
        }

        /// <summary>
        /// Verifies that message was received (looks at the newest one)
        /// </summary>
        public void ReceiveEmail()
        {
            Logger.WriteLine(new[] { "\"receive email\" test: " });
            if (LogIn() == false)
            {
                Logger.WriteLine(new[] { "FAILED" });
                return;
            }
            email = new MailPage(browser, Program.Settings["username"], "Test Message", testMessageSubject);
            string receivedMessageSubject = testMessageSubject;
            if (Program.Settings["test_to_run"] == Program.RECEIVE_EMAIL_TEST)
            {
                receivedMessageSubject = Program.Settings["test_message"].ToString();
            }
            string lastmessage = email.GetLastReceivedMessage();
            try
            {
                Assert.IsTrue(lastmessage.Equals(receivedMessageSubject));
                Logger.WriteLine(new[] { "OK" });
            }
            catch (AssertionException)
            {
                Logger.WriteLine(new[] { "Message was not received. ", "\""+receivedMessageSubject + "\" expected, but \"" + lastmessage + "\" found.", "FAILED" });
            }
            finally
            {
                email.LogOut();
            }
        }

        private bool LogIn()
        {
            bool loggedIn = false;
            logIn = new LoginPage(browser, Program.Settings["username"], Program.Settings["password"]);
            if (logIn.GetPageTitle().Contains("Gmail") == false)
            {
                string appendText = "Unable to reach gmail.com website. Please, check your Internet connection";
                Logger.WriteLine(new[] { appendText});
                return loggedIn;
            }
            if (logIn.LoginTxtBox.IsElementDiplayed())
            {
                logIn.EnterUserName();
                logIn.MoveForward();
            }
            try
            {
                Assert.IsFalse(logIn.IsLoginErrorMessagePresent());
            }
            catch
            {
                Logger.WriteLine(new[] { logIn.GetLoginErrorMessage() });
                return loggedIn;
            }
            logIn.EnterPassword();
            logIn.LogIn();
            try
            {
                Assert.IsFalse(logIn.IsPasswordErrorMessagePresent());
            }
            catch (AssertionException)
            {
                Logger.WriteLine(new[] { logIn.GetPasswordErrorMessage() });
                return loggedIn;
            }
            loggedIn = true;
            return loggedIn;
        }
    }
}
