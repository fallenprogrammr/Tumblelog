using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using TechTalk.SpecFlow;


namespace Tumblelog.Tests {
    [Binding]
    public class TumblelogViewSteps {
        private IWebDriver driver;

        [Given(@"I have to view the home page")]
        [DeploymentItem("CleanupTables.sql")]
        [DeploymentItem("HomePageTestData.sql")]
        public void GivenIHaveToViewTheHomePage() {
            driver = new PhantomJSDriver();
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
            var scriptRunner = new SqlServerCeRunner.Runner(ConfigurationManager.ConnectionStrings["tumblelogDb"].ConnectionString);
            scriptRunner.RunFile("CleanupTables.sql");
            scriptRunner.RunFile("HomePageTestData.sql");            
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

        [Then(@"the page should display the summaries of the latest (.*) blog entries")]
        public void ThenThePageShouldDisplayTheSummariesOfTheLatestBlogEntries(int p0) {
            Assert.AreEqual(p0, driver.FindElements(By.Id("recentBlogPosts")).Count);
            var headings = driver.FindElements(By.Id("postHeading"));
            foreach (var heading in headings) {
                Assert.IsTrue(heading.Text == "acceptance-test-post7" || heading.Text == "acceptance-test-post6" || heading.Text == "acceptance-test-post5" || heading.Text == "acceptance-test-post4" || heading.Text == "acceptance-test-post3");
            }
        }

        [Then(@"the total count of entries in the database should match the total count shown on the home page")]
        public void ThenTheTotalCountOfEntriesInTheDatabaseShouldMatchTheTotalCountShownOnTheHomePage() {
            const int expectedTotalPosts = 7;
            Assert.IsTrue(driver.FindElement(By.Id("totalBlogPosts")).Text.Equals(expectedTotalPosts + " total blog posts so far."));
        }

    }
}
