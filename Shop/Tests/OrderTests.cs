using Newtonsoft.Json;
using OpenQA.Selenium;
using RestSharp;
using Shop.Models;
using Shop.Pages;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Shop.Tests
{
    internal class OrderTests : Main
    {
        [Test]
        public void VerifyIfAddingToCartProductWorks()
        {
            ProductPage productPage = new ProductPage(Driver, Wait);

            productPage.loadProducts();
            productPage.orderProduct(1);
            
            OrderPage orderPage = new OrderPage(Driver, Wait);

            orderPage.OpenOrderPage();
            orderPage.loadProducts();

            Assert.That(orderPage.products.ElementAt(0).Equals(productPage.products.ElementAt(0)));
        }

        [Test]
        public void VerifyIfDeletingProductFromCartWorks()
        {
            ProductPage productPage = new ProductPage(Driver, Wait);

            productPage.loadProducts();
            productPage.orderProduct(3);
            productPage.orderProduct(1);

            OrderPage orderPage = new OrderPage(Driver, Wait);

            orderPage.OpenOrderPage();
            orderPage.loadProducts();

            ProductModel deletedProduct = orderPage.products.ElementAt(0);
            orderPage.deleteProduct(deletedProduct.Id);
            orderPage.products.Clear();
            Thread.Sleep(5000);

            orderPage.loadProducts();

            Assert.That(!orderPage.products.ElementAt(0).Equals(deletedProduct));
        }

        [Test]
        public void VerifyIfPlacingOrderWorks()
        {
            ProductPage productPage = new ProductPage(Driver, Wait);

            productPage.loadProducts();
            productPage.orderProduct(3);
            productPage.orderProduct(1);

            OrderPage orderPage = new OrderPage(Driver, Wait);

            orderPage.OpenOrderPage();
            orderPage.PlaceOrder();

            RestClient client = new RestClient("https://randomuser.me/");
            RestRequest request = new RestRequest("api", Method.Get);

            RestResponse response = client.Execute(request);

            dynamic userData = JsonConvert.DeserializeObject<dynamic>(response.Content);

            var userName = userData.results[0].name;
            var userLocation = userData.results[0].location;
            CardModel userCard = new CardModel("1234567890123456", "10", "2007");

            orderPage.FillOrderFields(new UserModel(userName.first, userName.last, userLocation.country, userLocation.city, userCard));     

            try
            {
                IWebElement successIcon = orderPage.successfulPurchaseIcon;
                Assert.Pass();
            } catch (NoSuchElementException e)
            {
                Assert.Fail();
            }

            Assert.Fail();
        }
    }
}
