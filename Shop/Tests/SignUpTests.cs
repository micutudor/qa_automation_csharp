using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using SeleniumExtras.WaitHelpers;
using Shop.Pages;

namespace Shop.Tests
{
    internal class SignUpTests : Main
    {
        [Test]
        public void VerifyIfSignUpWorks()
        {
            SignUpPage signUp = new SignUpPage(Driver, Wait);

            RestClient client = new RestClient("https://randomuser.me");
            RestRequest request = new RestRequest("/api", Method.Get);
            RestResponse response = client.Execute(request);

            dynamic userData = JsonConvert.DeserializeObject<dynamic>(response.Content);

            var userName = userData.results[0].name;

            signUp.OpenSignUpModal();
            signUp.SignUp(userName.first + " " + userName.last, "Tudor0*");

            IAlert alert = Wait.Until(ExpectedConditions.AlertIsPresent());
            string alertText = alert.Text;
            alert.Accept();

            Assert.That(alertText, Is.EqualTo("Sign up successful."));
        }

        [Test]
        public void VerifyIfYouCanSignUpWithAnUsedUsername()
        {
            SignUpPage signUp = new SignUpPage(Driver, Wait);

            signUp.OpenSignUpModal();
            signUp.SignUp("tudoricaexpertulafemei", "Tudor0*");

            IAlert alert = Wait.Until(ExpectedConditions.AlertIsPresent());
            string alertText = alert.Text;
            alert.Accept();

            Assert.That(alertText, Is.EqualTo("This user already exist."));
        }
    }
}
