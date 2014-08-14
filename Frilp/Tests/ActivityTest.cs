﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Frilp
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class ActivityTest<TWebDriver>: TestBase<TWebDriver> where TWebDriver : IWebDriver , new()
    {
        [Test]
        public void ApplyQuestionFilter()
        {
            string searchText = "restaurant";
            SearchResults<string, string> getsearchresults = new SearchResults<string, string>();
            List<string> entity = new List<string>();
            entity.Add("14");
            int count = 0;
            Login login = new Login(driver, LoginType.Facebook);
            login.SignIn();
            FrilpHome home = new FrilpHome(driver);
            SeleniumHelper.ImplicitWait(driver, TimeSpan.FromSeconds(15));
            home.Search.SendKeys(searchText);
            SeleniumHelper.ImplicitWait(driver, TimeSpan.FromSeconds(25));
            home.SelectAutoCompleteSearch(searchText);
            FrilpBusiness business = new FrilpBusiness(driver);
            business.WaitForResults(searchText, TimeSpan.FromSeconds(20));
            home.ActivityTab.Click();
            FrilpActivity activity = new FrilpActivity(driver);
            activity.QuestionFilter.Click();
            count=SeleniumHelper.ScrollWebPageGetCount(driver, By.ClassName("d_activity_list_item"));
            getsearchresults=activity.GetInvalidSearchResults(entity, count);
            Assert.IsFalse(getsearchresults.duplicateandInvalidItems.Count > 0 || getsearchresults.invalidItems.Count > 0);
        }
        [TearDown]
        public void SignOut()
        {
            FrilpHome home = new FrilpHome(driver);
            home.SignOut();
        }
    }
}
