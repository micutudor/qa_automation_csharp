using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Pages
{
    class ProductPage
    {
        public ProductPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        private IWebDriver _driver;
        private WebDriverWait _wait;

        public List<ProductModel> products = new List<ProductModel>();

        public IWebElement addToCartButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[text()='Add to cart']")));

        public void AddToCart() => addToCartButton.Click();

        public ReadOnlyCollection<IWebElement> GetCurrentPageProductsCards() 
            => _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='card h-100']")));

        public string getProductName(IWebElement productCard) => productCard.FindElement(By.XPath(".//a[@class='hrefch']")).Text;

        public string getProductPrice(IWebElement productCard) => productCard.FindElement(By.XPath(".//h5")).Text;

        public void loadProducts()
        {
            ReadOnlyCollection<IWebElement> productsCards
                = GetCurrentPageProductsCards();

            int i = 1;
            foreach (IWebElement productCard in productsCards)
                products.Add(new ProductModel(i++, getProductName(productCard), getProductPrice(productCard)));
        }

        public void orderProduct(int productId)
        {
            _driver.Navigate().GoToUrl("https://www.demoblaze.com/prod.html?idp_=" + productId);
            AddToCart();
            _wait.Until(ExpectedConditions.AlertIsPresent()).Accept();  
        }
    }
}
