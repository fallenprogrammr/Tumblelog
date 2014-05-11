using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using TechTalk.SpecFlow;

namespace Tumblelog.Tests {
    [Binding]
    public class TumblelogViewSteps {
        private IWebDriver driver;

        [Given(@"I have to view the home page")]
        public void GivenIHaveToViewTheHomePage() {
            driver = new PhantomJSDriver();
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
        }

        [When(@"I browse to the home page")]
        public void WhenIBrowseToTheHomePage() {
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["home"]);
        }

        [Then(@"the page should have the title of '(.*)'")]
        public void ThenThePageShouldHaveTheTitleOf(string p0) {
            Assert.AreEqual(p0, driver.Title);
        }

        [Then(@"the heading on the page should be '(.*)'")]
        public void ThenTheHeadingOnThePageShouldBe(string p0) {
            Assert.AreEqual(p0, driver.FindElement(By.Id("PageHeading")).Text);
        }
    }
}
