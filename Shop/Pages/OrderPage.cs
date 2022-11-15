using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Pages
{
    class OrderPage
    {
        public OrderPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        private IWebDriver _driver;
        private WebDriverWait _wait;

        public IWebElement cartLink => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("cartur")));

        public void OpenOrderPage() => cartLink.Click();

        public IWebElement orderButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Place Order']")));

        public void PlaceOrder() => orderButton.Click();

        public IWebElement nameInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("name")));

        public IWebElement countryInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("country")));

        public IWebElement cityInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("city")));

        public IWebElement cardInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("card")));

        public IWebElement monthInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("month")));

        public IWebElement yearInput => _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("year")));

        public IWebElement purchaseButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Purchase']")));

        public void Purchase() => purchaseButton.Click();

        public void FillOrderFields(UserModel user)
        {
            nameInput.SendKeys(user.FirstName + " " + user.LastName);
            countryInput.SendKeys(user.Country);
            cityInput.SendKeys(user.City);
            cardInput.SendKeys(user.Card.Number);
            monthInput.SendKeys(user.Card.ExpiringMonth);
            yearInput.SendKeys(user.Card.ExpiringYear);
            Purchase();
        }

        public IWebElement successfulPurchaseIcon => _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='sa-icon sa-success animate']")));

        public IWebElement orderTotal => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("totalp")));

        public int Total() => int.Parse(orderTotal.Text.Trim('$'));

        public List<ProductModel> products = new List<ProductModel>();

        public IReadOnlyCollection<IWebElement> GetCurrentPageOrderedProducts()
            => _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//tr[@class='success']")));

        private IWebElement getProductRow(int productId) =>
            GetCurrentPageOrderedProducts().ElementAt(productId);

        private IWebElement getRowCell(IWebElement row, int cell) => row.FindElements(By.XPath(".//td")).ElementAt(cell - 1);

        private string getProductName(IWebElement productRow) => getRowCell(productRow, 2).Text;

        private string getProductPrice(IWebElement productRow) => getRowCell(productRow, 3).Text;

        public void deleteProduct(int productId) => getRowCell(getProductRow(productId), 4).FindElement(By.XPath(".//a")).Click();

        public void loadProducts()
        {
            IReadOnlyCollection<IWebElement> orderedProducts = GetCurrentPageOrderedProducts();

            int i = 0;
            foreach (IWebElement orderedProduct in orderedProducts)
                products.Add(new ProductModel(i++, getProductName(orderedProduct), getProductPrice(orderedProduct)));
        }
    }
}
