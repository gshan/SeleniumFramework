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
    public class FrilpHome
    {
        private IWebDriver _driver;

        public FrilpHome(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement HomeButton
        {
            get
            {
                return _driver.FindElement(By.ClassName("logo_new"));
            }
        }
        public IWebElement Search
        {
            get
            {
                return _driver.FindElement(By.Id("main_search_box"));
            }
        }
        public IWebElement BusinessTab
        {
            get
            {
                return _driver.FindElement(By.ClassName("tab_businesses"));
            }
        }
        public IWebElement ActivityTab
        {
            get
            {
                return _driver.FindElement(By.ClassName("tab_activities"));
            }
        }
        public IWebElement AskQuestion
        {
            get
            {
                return _driver.FindElement(By.XPath("//div[@class='nav-top']//li[@id='link_ask']"));
            }
        }
        //public void AskQuestion()
        //{
        //    SeleniumHelper.JavaScriptExecutor(_driver, "document.getElementById('link_ask').click()");
        //}
        public void SelectAutoCompleteSearch(string text)
        {
            _driver.FindElement(By.XPath(string.Format("//*[@class='d_autocomplete_result_name' and contains(text(),'{0}')]",text))).Click();
        }
        public IWebElement Profile
        {
            get
            {
                return _driver.FindElement(By.ClassName("profile"));
            }
        }
        public IWebElement Signout
        {
            get
            {
                return _driver.FindElement(By.Id("link_signout"));
            }
        }
        public void SignOut()
        {
            Profile.Click();
            Signout.Click();
        }
    }
}
