using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Frilp
{
    public class FrilpAskQuestion
    {
        private IWebDriver _driver;
        public FrilpAskQuestion(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement MessageBox
        {
            get
            {
                return _driver.FindElement(By.Id("ask_message"));
            }
        }
        public IWebElement Category
        {
            get
            {
                return _driver.FindElement(By.Id("ask_category_chooser"));
            }
        }
        public void SelectCategory(string text)
        {
            SeleniumHelper.ImplicitWait(_driver, TimeSpan.FromSeconds(25));
            _driver.FindElement(By.XPath(string.Format("//*[@class='d_autocomplete_result_name' and contains(text(),'{0}')]", text))).Click();
        }
        public IWebElement Location
        {
            get
            {
                return _driver.FindElement(By.Id("ask_location_chooser"));
            }
        }
        public void SelectLocation(string text)
        {
            SeleniumHelper.ImplicitWait(_driver, TimeSpan.FromSeconds(25));
            _driver.FindElement(By.XPath(string.Format("//*[@class='d_autocomplete_result_name' and contains(text(),'{0}')]", text))).Click();
        }
        public IWebElement TagUser
        {
            get
            {
                return _driver.FindElement(By.Id("usertaggerbox"));
            }
        }
        public void SelectUser(string text)
        {
            SeleniumHelper.ImplicitWait(_driver, TimeSpan.FromSeconds(25));
            _driver.FindElement(By.XPath(string.Format("//*[@class='d_usertagger_result_name' and contains(text(),'{0}')]", text.ToLower()))).Click();
        }
        public IWebElement AskButton
        {
            get
            {
                return _driver.FindElement(By.XPath("//div[contains(@class,'btn2') and contains(text(),'Ask')]"));
            }
        }
        public IWebElement OkButton
        {
            get
            {
                return _driver.FindElement(By.XPath("//div[contains(@class,'btn2') and contains(text(),'Ok')]"));
            }
        }
        public bool WaitForPublishedQuestion(string text)
        {
            bool isvisible = false;
            int limit = 0;
            FrilpActivity activity = new FrilpActivity(_driver);
            while (!isvisible)
            {
                SeleniumHelper.ImplicitWait(_driver, TimeSpan.FromSeconds(5));
                isvisible = _driver.FindElements(By.XPath(string.Format("//div[contains(@class,'postcontent')]/p[contains(text(),'{0}')]", text))).Count > 0;
                activity.QuestionFilter.Click();
                limit++;
                if (limit > 30)
                    isvisible = true;
            }
            if (limit > 30)
                return false;
            return true;
        }

    }
}
