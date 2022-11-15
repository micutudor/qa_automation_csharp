using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Pages
{
    class LogInPage
    {
        public LogInPage(IWebDriver driver, WebDriverWait wait)
        {
            this._driver = driver;
            this._wait = wait;
        }

        public IWebDriver _driver { get; }

        public WebDriverWait _wait { get; }

        public IWebElement loginLink => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("login2")));

        public IWebElement usernameInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginusername")));

        public IWebElement passwordInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginpassword")));

        public IWebElement loginButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Log in']")));

        public void OpenLogInModal() => loginLink.Click();

        public void LogIn(string username, string password)
        {
            usernameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            loginButton.Click();
        }
    }
}
