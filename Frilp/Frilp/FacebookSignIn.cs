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
    public class FacebookSignIn
    {
        private IWebDriver _driver;

        public FacebookSignIn(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement EmailId
        {
            get
            {
                return _driver.FindElement(By.Id("email"));
            }
        }
        public IWebElement Password
        {
            get
            {
                return _driver.FindElement(By.Id("pass"));
            }
        }
        public IWebElement SignIn
        {
            get
            {
                return _driver.FindElement(By.Name("login"));
            }
        }

        public void Login(string email, string password)
        {
            bool isloginreqd = SeleniumHelper.IsElementPresent(_driver, By.Id("email"), TimeSpan.FromSeconds(7));
            if (isloginreqd)
            {
                EmailId.Clear();
                EmailId.SendKeys(email);
                Password.Clear();
                Password.SendKeys(password);
                SignIn.Click();
            }
        }
    }
}
