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
    public class AskQuestionTest<TWebDriver> :TestBase<TWebDriver> where TWebDriver : IWebDriver , new()
    {
        [Test]
        public void AskQuestion()
        {
            try
            {
                string questiontext = "Looking for a Chinese Restaurant";
                string category = "restaurant";
                string location = "Adyar";
                string user = "manishankar";
                Login login = new Login(driver, LoginType.Facebook);
                login.SignIn();
                FrilpHome home = new FrilpHome(driver);
                home.ActivityTab.Click();
                FrilpActivity activity = new FrilpActivity(driver);
                activity.QuestionFilter.Click();
                SeleniumHelper.ExplicitWait(driver, By.XPath("//div[@class='nav-top']//li[@id='link_ask']"), TimeSpan.FromSeconds(25));
                home.AskQuestion.Click();
                //driver.FindElement(By.XPath("//div[@class='nav-top']//li[@id='link_ask']")).Click();
                FrilpAskQuestion postquestion = new FrilpAskQuestion(driver);
                postquestion.MessageBox.SendKeys(questiontext);
                postquestion.Category.SendKeys(category);
                postquestion.SelectCategory(category);
                postquestion.Location.SendKeys(location);
                postquestion.SelectLocation(location);
                postquestion.TagUser.SendKeys(user);
                postquestion.SelectUser(user);
                postquestion.AskButton.Click();
                postquestion.OkButton.Click();
                Assert.IsTrue(postquestion.WaitForPublishedQuestion(questiontext));
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
