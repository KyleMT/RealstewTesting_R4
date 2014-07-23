using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;


namespace RealstewTestingResources
{
    public static class Masterpage
    {
        public static void Login(IWebDriver driver, string userName = "kyle.mark.taylor17@gmail.com", string password = "gnomer1552")
        {
            if (!IsLoggedIn(driver))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                wait.IgnoreExceptionTypes(typeof(TimeoutException));
                int attemptCounter = 0;

                IWebElement userBar = wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar));
                do
                {
                    if (attemptCounter > 3) throw new Exception("Max loggin attempts reached");
                    attemptCounter++;
                    userBar.Click();
                } while (wait.Until(ExpectedConditions.ElementIsVisible(UIMap.LoginForm.LoginBody)) == null);

                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.LoginForm.InputField_Email)).SendKeys(userName);
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.LoginForm.InputField_Password)).SendKeys(password);
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.LoginForm.SubmitBtn)).Click();
            }
        }

        public static bool IsLoggedIn(IWebDriver driver)
        {
            CustomConditions.WaitForAjax(driver, 2000);
            return (driver.FindElement(UIMap.NavigationBar.UserBar).GetAttribute("title") != "") ? true : false;
        }




    }
}
