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
    public class TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        public IWebDriver driver ;
        [SetUp]
        public void CreateDriver()
        {
            if (typeof(TWebDriver) == typeof(FirefoxDriver))
            {
                driver = new FirefoxDriver();
            }
            else if (typeof(TWebDriver) == typeof(ChromeDriver))
            {
                driver = new ChromeDriver();
            }
            else
            {
                var options = new InternetExplorerOptions();
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                driver = new InternetExplorerDriver(options);
            }
        }
        [TearDown]
        public void QuitDriver()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
