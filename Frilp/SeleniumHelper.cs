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
    public static class SeleniumHelper
    {
        public static int ScrollWebPageGetCount(IWebDriver driver, OpenQA.Selenium.By locator)
        {
            bool isend = false;
            int count = 0;
            int prev_count = 0;
            int limit = 0;
            int maxlimit = 5;

            while (!isend)
            {
                //String script2 = "window.scrollTo(0,Math.max(document.documentElement.scrollHeight," + "document.body.scrollHeight,document.documentElement.clientHeight));";
                //js2.ExecuteScript(script2);
                GotoEnd(driver);
                ImplicitWait(driver, TimeSpan.FromSeconds(2));
                count = driver.FindElements(locator).Count;
                try
                {
                    isend = driver.FindElements(By.ClassName("end-card")).Count > 0;
                }
                catch (WebDriverTimeoutException)
                {
                    isend = true;
                    throw new OpenQA.Selenium.WebDriverTimeoutException();
                }
                if (isend == false && count == prev_count)
                {
                    limit++;
                    if (limit > maxlimit)
                        isend = true;
                }
                else
                {
                    limit = 0;
                }
                prev_count = count;
            }
            return count;
        }
        public static void SwitchWindow(IWebDriver driver,string handle)
        {
            driver.SwitchTo().Window(handle);
        }
        public static string GetPopUpHandle(IWebDriver driver,string currentwindow)
        {
            var handles = driver.WindowHandles;
            string popupHandle = string.Empty;
            foreach (var handle in handles)
            {
                if (handle != currentwindow)
                {
                    popupHandle = handle; break;
                }
            }
            return popupHandle;
        }
        public static IWebElement JavaScriptExecutor(IWebDriver driver, string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            return (IWebElement)js.ExecuteScript(script);
        }

        //Explicit Wait
        public static void ExplicitWait(IWebDriver driver, OpenQA.Selenium.By locator, TimeSpan T)
        {
            WebDriverWait wait = new WebDriverWait(driver, T);
            IWebElement elementToWait = wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static bool IsElementPresent(IWebDriver driver,OpenQA.Selenium.By locator, TimeSpan T)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, T);
                IWebElement elementToWait = wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static void ImplicitWait(IWebDriver driver, TimeSpan T)
        {
            driver.Manage().Timeouts().ImplicitlyWait(T);
        }

        //Maximize Window
        public static void MaximizeWindow(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

        //Navigate
        public static void Navigate(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        //This function is used to close all open browser window
        public static void CloseWebDriver(IWebDriver driver)
        {
            driver.Quit();
        }

        //This function is used to close to the browser window
        public static void CloseBrowser(IWebDriver driver)
        {
            driver.Close();
        }
        public static void GotoEnd(IWebDriver driver)
        {
            FrilpHome home = new FrilpHome(driver);
            home.HomeButton.SendKeys(Keys.End);
        }
        public static void GotoHome(IWebDriver driver)
        {
            FrilpHome home = new FrilpHome(driver);
            home.HomeButton.SendKeys(Keys.Home);
        }
    }
}
