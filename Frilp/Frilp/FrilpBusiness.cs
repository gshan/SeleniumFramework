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
    public class FrilpBusiness
    {
        private IWebDriver _driver;
        public FrilpBusiness(IWebDriver driver)
        {
            _driver = driver;
        }


        public void WaitForResults(string text,TimeSpan t)
        {
            var locator = By.XPath(string.Format("//div[@id='search_count_text']/*[contains(text(),'{0}')]", text));
            SeleniumHelper.ExplicitWait(_driver,locator,t);
        }
        public SearchResults<string, string> GetInvalidSearchResults(List<string> categorylist,int count)
        {
            SearchResults<string, string> getSearchResults = new SearchResults<string, string>();
            string businessname = string.Empty;
            string businessid = string.Empty;
            bool isvalidcategory;
            for (int i = 1; i < count; i++)
            {
                var getcategory = _driver.FindElements(By.XPath("(//div[@class='business-name'])[" + i + "]//a[@class='category_filter']"));
                var getbusinessinfo = _driver.FindElement(By.XPath("(//div[@class='business-name'])[" + i + "]//a[@class='business_profile']"));
                businessid = getbusinessinfo.GetAttribute("business_id");
                businessname = getbusinessinfo.Text;
                var getcategoryid = getcategory.Select(c => c.GetAttribute("category_id"));
                isvalidcategory = getcategoryid.Any(a => categorylist.Any(c=> c==a));
                if (!isvalidcategory)
                {

                    try
                    {
                        getSearchResults.invalidItems.Add(businessid, businessname);
                    }
                    catch (ArgumentException)
                    {
                        if (!getSearchResults.duplicateandInvalidItems.Contains(businessid))
                            getSearchResults.duplicateandInvalidItems.Add(businessid);
                    }
                }
                else
                {
                    try
                    {
                        getSearchResults.validItems.Add(businessid, businessname);
                    }
                    catch (ArgumentException)
                    {
                        if (!getSearchResults.duplicateItems.Contains(businessid))
                            getSearchResults.duplicateItems.Add(businessid);
                    }
                }
            }
            return getSearchResults;
        }
    }
}
