﻿using System;
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
        public static void Login(IWebDriver driver, string userName = "rshq.test@gmail.com", string password = "201401")
        {
            if (!IsLoggedIn(driver))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));
                int attemptCounter = 0;

                IWebElement userBar = wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar));
                if (userBar == null) throw new Exception("Userbar not found");
                do
                {
                    if (attemptCounter > 3) throw new Exception("Max login attempts reached");
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
            CustomConditions.WaitForAjax(driver, 1000);

            try
            {
                IWebElement userBar = driver.FindElement(UIMap.NavigationBar.UserBar);
                return (userBar.GetAttribute("title") != "") ? true : false;
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Cannot find userBar element, failed to login");
            }
        }
    }
}
