using OpenQA.Selenium;

namespace TestMail
{
    public class LoginPage
    {        
        private Browser webbrowser;

        private TextBox loginTextBox, psswdTextBox;
        private Button nextButton, loginButton;
        private WebElement errorLoginMsgTxt, errorPasswdMsgTxt;

        public LoginPage(Browser browser, string username, string psswd)
        {
            webbrowser = browser;
            loginTextBox = new TextBox(webbrowser, By.Id("Email"), username);
            psswdTextBox =new TextBox(webbrowser, By.Id("Passwd"), psswd);
            nextButton = new Button(webbrowser, By.Id("next"));
            loginButton = new Button(webbrowser, By.Id("signIn"));
            errorLoginMsgTxt = new WebElement(webbrowser, By.Id("errormsg_0_Email"));
            errorPasswdMsgTxt = new WebElement(webbrowser, By.Id("errormsg_0_Passwd"));
        }

        public TextBox LoginTxtBox
        {
            get { return loginTextBox; }
        }        

        public void EnterUserName()
        {            
            loginTextBox.EnterText();
            return;
        }

        public void EnterPassword()
        {            
            psswdTextBox.EnterText();
            return;
        }

        public void LogIn()
        {
            loginButton.ClickOnButton();
            return;
        }

        public void MoveForward()
        {           
            nextButton.ClickOnButton();
            return;
        }    

        public bool IsLoginErrorMessagePresent()
        {            
            return errorLoginMsgTxt.IsElementDiplayed();
        }

        public string GetLoginErrorMessage()
        {            
            return errorLoginMsgTxt.GetElementText();
        }

        public bool IsPasswordErrorMessagePresent()
        {
            return errorPasswdMsgTxt.IsElementDiplayed();
        }

        public string GetPasswordErrorMessage()
        {
            return errorPasswdMsgTxt.GetElementText();
        }

        public string GetPageTitle()
        {
            return webbrowser.Driver.Title;
        }
       
    }
}
