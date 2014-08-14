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
    public class Login
    {
        private IWebDriver _driver;
        private LoginType _logintype;
        public Login(IWebDriver driver, LoginType logintype)
        {
            _driver = driver;
            _logintype = logintype;
        }
        public IWebElement FrilpSignIn
        {
            get
            {
                return _driver.FindElement(By.Id("btn_signin"));
            }
        }
        public void CloseSignInPopup()
        {
            bool ispopuppresent = _driver.FindElements(By.Id("popup_btn_signin")).Count > 0;
            if (ispopuppresent)
                _driver.FindElement(By.Id("popup_close_signin")).Click();
        }
        public void SignIn()
        {
            SeleniumHelper.Navigate(_driver, "http://frilp.com");
            SeleniumHelper.MaximizeWindow(_driver);

            switch (_logintype)
            {
                case LoginType.Facebook:
                    string currentwindow = _driver.CurrentWindowHandle;
                    CloseSignInPopup();
                    FrilpSignIn.Click();
                    string popuphandle = SeleniumHelper.GetPopUpHandle(_driver, currentwindow);
                    SeleniumHelper.SwitchWindow(_driver, popuphandle);
                    FacebookSignIn fb = new FacebookSignIn(_driver);
                    fb.Login("Manishankar2", "Maverick1234");
                    SeleniumHelper.SwitchWindow(_driver, currentwindow);
                    break;
            }
        }
    }
}
