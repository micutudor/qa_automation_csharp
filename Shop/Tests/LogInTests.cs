using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Shop.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Tests
{
    internal class LogInTests : Main
    {
        [Test]
        public void VerifyIfLogInWorks()
        {
            LogInPage logIn = new LogInPage(Driver, Wait);
            
            logIn.OpenLogInModal();
            logIn.LogIn("tudoricaexpertulafemei", "Tudor0*");

            try
            {
                Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("logout2")));
                Assert.Pass();
            } 
            catch (ElementNotVisibleException e)
            {
                Assert.Fail();
            }
        }
    }
}
