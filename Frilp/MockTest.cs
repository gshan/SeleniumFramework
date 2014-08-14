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
    public class MockTest
    {
        public static void LoginTest()
        {
            Console.WriteLine(string.Format("'{0}'","manishankar"));
            IWebDriver driver = new FirefoxDriver();
            Login frilplogin = new Login(driver,LoginType.Facebook);
            frilplogin.SignIn();
            driver.FindElement(By.XPath("//li[contains(@class,'category_menu_item') and @category_id='1']")).Click();
        }
        public static void EntireFlow()
        {
            IWebDriver driver;
           // var options = new InternetExplorerOptions();
            //options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            //driver = new InternetExplorerDriver(options);
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://frilp.com");
            driver.Manage().Window.Maximize();
            string currentwindow = driver.CurrentWindowHandle;
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //String script = "document.getElementById('btn_signin').click()";
            //js.ExecuteScript(script);
            //Thread.Sleep(2000); 
            driver.FindElement(By.Id("btn_signin")).Click();
            var handles = driver.WindowHandles;
            string popupHandle = string.Empty;
            foreach (var handle in handles)
            {
                if (handle != currentwindow)
                {
                    popupHandle = handle; break;
                }  
            }
            driver.SwitchTo().Window(popupHandle);

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Duration));

            DefaultWait<IWebDriver> wait =  new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(5);
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException),typeof(WebDriverTimeoutException));
            bool isLogin = false;
            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Id("email")));
            }
            catch (WebDriverTimeoutException)
            {
                isLogin = true;
            }
            catch (NoSuchElementException)
            {
                isLogin = true;
            }

            if (!isLogin)
            {
                driver.FindElement(By.Id("email")).Clear();
                driver.FindElement(By.Id("email")).SendKeys("Manishankar2");
                driver.FindElement(By.Id("pass")).SendKeys("Maverick1234");
                driver.FindElement(By.Name("login")).Click();
                driver.SwitchTo().Window(currentwindow);
            }

            WebDriverWait wait5 = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
            wait5.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='nav-top']//li[@id='link_ask']")));

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25));
            //driver.FindElement(By.Id("main_search_box")).SendKeys("restaurants");
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25));
            //driver.FindElement(By.XPath("//*[@class='d_autocomplete_result_name' and contains(text(),'restaurants')]")).Click();

            ////driver.FindElement(By.ClassName("d_autocomplete_result_name")).Click();
            ////driver.FindElement(By.Id("main_search_box")).SendKeys(Keys.Enter);

            //WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            //IWebElement elementToWait = wait1.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='search_count_text']/*[contains(text(),'restaurants')]")));



            ////driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(7000));
            ////IJavaScriptExecutor js1 = (IJavaScriptExecutor)driver;
            ////String script1 = "document.getElementsByClassName('d_autocomplete_result_name')[0]";
            ////string firstresult =(string)js1.ExecuteScript(script1);
            ////if (firstresult.Trim().ToLower() == "restaurants")
            ////{
            ////    IJavaScriptExecutor js2 = (IJavaScriptExecutor)driver;
            ////    String script2 = "document.getElementsByClassName('d_autocomplete_result_name')[0].click()";
            ////    js2.ExecuteScript(script2);
            ////}

            //// Scroll Logic
            //int count = ScrollWebPage(driver, By.ClassName("business-name"));
            //// Check category and Duplicates
            //Dictionary<string, string> Restaurant = new Dictionary<string, string>();
            //Dictionary<string, string> InvalidCategory = new Dictionary<string, string>();
            //List<string> duplicate = new List<string>();
            //List<string> duplicateandInvalidcategory = new List<string>();
            //string businessname = string.Empty;
            //string businessid = string.Empty;

            //bool isrestaurant;
            //for (int i = 1; i < count; i++)
            //{
            //    var getcategory = driver.FindElements(By.XPath("(//div[@class='business-name'])[" + i + "]//a[@class='category_filter']"));
            //    var getbusinessinfo = driver.FindElement(By.XPath("(//div[@class='business-name'])[" + i + "]//a[@class='business_profile']"));
            //    businessid = getbusinessinfo.GetAttribute("business_id");
            //    businessname = getbusinessinfo.Text;
            //    isrestaurant = getcategory.Any(l => l.GetAttribute("category_id") == "40");
            //    if (!isrestaurant)
            //    {

            //        try
            //        {
            //            InvalidCategory.Add(businessid, businessname);
            //        }
            //        catch (ArgumentException)
            //        {
            //            if (!duplicateandInvalidcategory.Contains(businessid))
            //                duplicateandInvalidcategory.Add(businessid);
            //        }
            //    }
            //    else
            //    {
            //        try
            //        {
            //            Restaurant.Add(businessid, businessname);
            //        }
            //        catch (ArgumentException)
            //        {
            //            if (!duplicate.Contains(businessid))
            //                duplicate.Add(businessid);
            //        }
            //    }
            //}

            ////Back to the top
            //driver.FindElement(By.Id("btn_top")).Click();

            // Select Questions in activity

            //driver.FindElement(By.ClassName("tab_activities")).Click();
            //driver.FindElement(By.XPath("//li[@class='d_activity_entity_filter' and @entity_type='14']")).Click();
            //int count1 = ScrollWebPage(driver,By.ClassName("d_activity_list_item"));

            //ASK Question
            driver.FindElement(By.XPath("//div[@class='nav-top']//li[@id='link_ask']")).Click();

            JavaScriptExecutor(driver, "document.getElementById('link_ask').click()");
            driver.FindElement(By.Id("ask_message")).SendKeys("Looking for a Chinese restaurant");
            driver.FindElement(By.Id("ask_category_chooser")).SendKeys("restaurant");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25));
            driver.FindElement(By.XPath("//*[@class='d_autocomplete_result_name' and contains(text(),'restaurants')]")).Click();

            driver.FindElement(By.Id("ask_location_chooser")).SendKeys("Adyar");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25));
            driver.FindElement(By.XPath("//*[@class='d_autocomplete_result_name' and contains(text(),'Adyar')]")).Click();

            driver.FindElement(By.Id("usertaggerbox")).SendKeys("Manishankar");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25));
            driver.FindElement(By.XPath("//*[@class='d_usertagger_result_name' and contains(text(),'manishankar ')]")).Click();
            
            driver.FindElement(By.XPath("//div[contains(@class,'btn2') and contains(text(),'Ask')]")).Click();
            driver.FindElement(By.XPath("//div[contains(@class,'btn2') and contains(text(),'Ok')]")).Click();

            
            //WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(150));
            //IWebElement elementToWait1 = wait2.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'postcontent')]/p[contains(text(),'restaurant')]")));

            driver.FindElement(By.ClassName("logo_new")).SendKeys(Keys.Home);

            bool isvisible = false;
            int limit =0;
            while (!isvisible)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                isvisible = driver.FindElements(By.XPath("//div[contains(@class,'postcontent')]/p[contains(text(),'restaurant')]")).Count > 0;
                driver.FindElement(By.XPath("//*[contains(@class,'d_activity_entity_filter') and @entity_type='14']")).Click();
                limit++;
                if (limit > 30)
                    isvisible = true;
            }

            //usertaggerbox
            // Signout
            driver.FindElement(By.ClassName("profile")).Click();
            driver.FindElement(By.Id("link_signout")).Click();
            
            //Close and Quit
            driver.Close();
            driver.Quit();
            //foreach(var invalid in InvalidCategory)
            //{
            //    Console.WriteLine(invalid.Value+ Environment.NewLine);
            //}
            Console.ReadLine();
        }

        public static int ScrollWebPage(IWebDriver driver,OpenQA.Selenium.By webelement)
        {
            bool isend = false;
            IJavaScriptExecutor js2 = (IJavaScriptExecutor)driver;
            int count = 0;
            int prev_count = 0;
            int count_prevcount_change = 0;
            while (!isend)
            {
                //String script2 = "window.scrollTo(0,Math.max(document.documentElement.scrollHeight," + "document.body.scrollHeight,document.documentElement.clientHeight));";
                //js2.ExecuteScript(script2);
                driver.FindElement(By.ClassName("logo_new")).SendKeys(Keys.End);
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                count = driver.FindElements(webelement).Count;
                try
                {
                    isend = driver.FindElements(By.ClassName("end-card")).Count > 0;
                }
                catch (WebDriverTimeoutException)
                {
                    isend = true;
                }
                if (isend == false && count == prev_count)
                {
                    count_prevcount_change++;
                    if (count_prevcount_change > 5)
                        isend = true;
                }
                else
                {
                    count_prevcount_change = 0;
                }
                prev_count = count;
            }
            return count;
        }

        public static void JavaScriptExecutor(IWebDriver driver,string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);
        }
    }
}
