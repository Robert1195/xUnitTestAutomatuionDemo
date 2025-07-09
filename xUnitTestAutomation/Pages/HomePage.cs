using OpenQA.Selenium;

namespace xUnitTestAutomation.Pages
{
    public interface IHomePage
    {
        void Login(string username, string password);
        bool isErrorMsgDisplayed();
        bool VerifyErrorMsgTxt();
    }

    public class HomePage(IDriverWait driver) : IHomePage
    {
        private IWebElement usernameField => driver.FindElement(By.Id("user-name"));
        private IWebElement passwordField => driver.FindElement(By.Id("password"));
        private IWebElement loginBtn => driver.FindElement(By.Id("login-button"));
        private IWebElement invalidPasswordErrorMsg => driver.FindElement(By.CssSelector("h3[data-test='error']"));

        public void Login(string username, string password)
        {
            usernameField.SendKeys(username);
            passwordField.SendKeys(password);
            loginBtn.Click();
        }

        public bool isErrorMsgDisplayed()
        {
            return invalidPasswordErrorMsg.Displayed;
        }

        public bool VerifyErrorMsgTxt()
        {
            return invalidPasswordErrorMsg.Text.Contains("Epic sadface:");
        }
    }
}
