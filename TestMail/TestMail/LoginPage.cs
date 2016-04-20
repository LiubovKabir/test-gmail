using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TestMail
{
    public class LoginPage
    {
        private WebElements wbElements;

        private By loginTextBox = By.Id("Email");
        private By nexButton = By.Id("next");
        private By psswdTextBox = By.Id("Passwd");
        private By loginButton = By.Id("signIn");
        private By errorLoginMsgTxt = By.Id("errormsg_0_Email");
        private By errorPasswdMsgTxt = By.Id("errormsg_0_Passwd");

        public LoginPage(WebElements wbElements)
        {
            this.wbElements = wbElements;
        }

        public void EnterUserName(String username)
        {
            wbElements.EnterText(loginTextBox, username);
        }

        public void EnterPassword(String passwd)
        {
            wbElements.EnterText(psswdTextBox, passwd);
        }

        public void LogIn()
        {
            wbElements.ClickOnButton(loginButton);
        }

        public void MoveForward()
        {
            wbElements.ClickOnButton(nexButton);
        }

        public void QuitBrowser()
        {
            wbElements.QuitBrowser();
        }      

        public bool IsLoginErrorMessagePresent()
        {
            return wbElements.IsElementDiplayed(errorLoginMsgTxt);
        }

        public string GetLoginErrorMessage()
        {
            return wbElements.GetElementText(errorLoginMsgTxt);
        }

        public bool IsPasswordErrorMessagePresent()
        {
            return wbElements.IsElementDiplayed(errorPasswdMsgTxt);
        }

        public string GetPasswordErrorMessage()
        {
            return wbElements.GetElementText(errorPasswdMsgTxt);
        }

       
    }
}
