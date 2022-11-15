using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace Shop
{
    class Main
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        [SetUp]
        public void Setup()
        {
            this.Driver = new ChromeDriver();
            this.Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.SwitchTo().DefaultContent();
            this.Driver.Close();
        }
    }
}