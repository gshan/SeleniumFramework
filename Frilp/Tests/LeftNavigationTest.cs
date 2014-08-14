using System;
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
    public class LeftNavigationTest<TWebDriver> : TestBase<TWebDriver> where TWebDriver: IWebDriver , new()
    {
        [Test]
        public void HomeandEssentialServicesTest()
        {
            try
            {
                Login login = new Login(driver, LoginType.Facebook);
                login.SignIn();
                FrilpHome home = new FrilpHome(driver);
                FrilpBusiness business = new FrilpBusiness(driver);
                List<string> category = new List<string>(new string[] { "66", "15", "184", "96", "357", "67", "59", "63", "190", "546", "96", "109", "186", "10", "57", "105", "102", "95", "92", "9", "15", "93", "103", "11" });
                int count = 0;
                SeleniumHelper.ImplicitWait(driver, TimeSpan.FromSeconds(15));
                FrilpLeftNavigation leftnav = new FrilpLeftNavigation(driver);
                leftnav.HomeandEssentialServices.Click();
                business.WaitForResults("Home & Essential services", TimeSpan.FromSeconds(25));
                count = SeleniumHelper.ScrollWebPageGetCount(driver, By.ClassName("business-name"));
                var getsearchResults = business.GetInvalidSearchResults(category, count);
                Assert.IsFalse(getsearchResults.duplicateandInvalidItems.Count > 0 || getsearchResults.invalidItems.Count > 0);
            }
            catch (WebDriverTimeoutException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TearDown]
        public void SignOut()
        {
            FrilpHome home = new FrilpHome(driver);
            home.SignOut();
        }
    }
}
