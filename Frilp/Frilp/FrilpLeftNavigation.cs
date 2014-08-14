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
    public class FrilpLeftNavigation
    {
        private IWebDriver _driver;
        public FrilpLeftNavigation(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement HomeandEssentialServices
        {
            get
            {
                return _driver.FindElement(By.XPath("//li[contains(@class,'category_menu_item') and @category_id='1']"));
            }
        }
    }
}
