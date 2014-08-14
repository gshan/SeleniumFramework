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
    public class FrilpBusinessTest<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver , new()
    {
        [Test]
        public void SearchBusinessCategory()
        {
            try
            {
                string searchText = "restaurant";
                List<string> category = new List<string>();
                category.Add("40");
                int count = 0;
                Login login = new Login(driver, LoginType.Facebook);
                login.SignIn();
                FrilpHome home = new FrilpHome(driver);
                SeleniumHelper.ImplicitWait(driver, TimeSpan.FromSeconds(15));
                home.Search.SendKeys(searchText);
                SeleniumHelper.ImplicitWait(driver, TimeSpan.FromSeconds(25));
                home.SelectAutoCompleteSearch(searchText);
                FrilpBusiness business = new FrilpBusiness(driver);
                business.WaitForResults(searchText,TimeSpan.FromSeconds(15));
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
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
