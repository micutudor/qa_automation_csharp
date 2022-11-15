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
    class SignUpPage
    {
        public SignUpPage(IWebDriver driver, WebDriverWait wait)
        {
            this._driver = driver;
            this._wait = wait;
        }

        private IWebDriver _driver { get; }

        private WebDriverWait _wait { get; }

        public IWebElement usernameInput { get => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("sign-username"))); }

        public IWebElement passwordInput { get => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("sign-password"))); }

        public IWebElement signUpButton { get => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()=\"Sign up\"]"))); }

        public IWebElement signUpLink { get => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("signin2"))); }

        public void OpenSignUpModal() => signUpLink.Click(); 

        public void SignUp(dynamic username, dynamic password)
        {
            usernameInput.SendKeys(Convert.ToString(username));
            passwordInput.SendKeys(password);
            signUpButton.Click();
        }
    }
}
