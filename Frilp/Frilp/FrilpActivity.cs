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
    public class FrilpActivity
    {
        private IWebDriver _driver ;
        public FrilpActivity (IWebDriver driver)
        {
            _driver= driver;
        }
        public IWebElement QuestionFilter
        {
            get
            {
                return _driver.FindElement(By.XPath("//li[contains(@class,'d_activity_entity_filter') and @entity_type='14']"));
            }
        }
        public SearchResults<string, string> GetInvalidSearchResults(List<string> filterlist, int count)
        {
            SearchResults<string, string> getSearchResults = new SearchResults<string, string>();
            string entitytype = string.Empty;
            string entityid = string.Empty;
            string user = string.Empty;
            string data = string.Empty;
            bool isfiltertype;
            for (int i = 1; i < count; i++)
            {
                var getactivityItem = _driver.FindElement(By.XPath("//li[@class='d_activity_list_item']["+i+"]"));
                var userelement = _driver.FindElement(By.XPath("//li[@class='d_activity_list_item'][" + i + "]//div[@class='top']//a[@target='_blank']"));
                var dataelement = _driver.FindElement(By.XPath("//li[@class='d_activity_list_item'][" + i + "]//div[@class='top']//a[2]"));
                user = userelement.Text;
                data = dataelement.Text;
                entitytype = getactivityItem.GetAttribute("entity_type");
                entityid = getactivityItem.GetAttribute("entity_id");
                isfiltertype = filterlist.Any(a => a == entitytype);
                if (!isfiltertype)
                {
                    try
                    {
                        getSearchResults.invalidItems.Add(entityid, string.Format("{0}:{1}",user,data));
                    }
                    catch (ArgumentException)
                    {
                        if (!getSearchResults.duplicateandInvalidItems.Contains(entityid))
                            getSearchResults.duplicateandInvalidItems.Add(entityid);
                    }
                }
                else
                {
                    try
                    {
                        getSearchResults.validItems.Add(entityid, string.Format("{0}:{1}", user, data));
                    }
                    catch (ArgumentException)
                    {
                        if (!getSearchResults.duplicateItems.Contains(entityid))
                            getSearchResults.duplicateItems.Add(entityid);
                    }
                }
            }
            return getSearchResults;
        }
    }
}
