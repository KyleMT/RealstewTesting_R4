using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace RealstewTestingResources
{
    public static class CustomConditions
    {
        public static void WaitForAjax(IWebDriver driver, int timeOut) //Timeout is in milliseconds.
        {
            try
            {
                //Polling too often may cause performance issues
                if (timeOut < 100)
                {
                    Console.WriteLine("Timeout too short. Using 100ms");
                    timeOut = 100;
                }

                //Check if browser can excecute JavaScript (Sanity check)
                if (driver is IJavaScriptExecutor)
                {
                    IJavaScriptExecutor jsDriver = (IJavaScriptExecutor)driver;

                    for (int i = 0; i <= (timeOut / 100); i++)
                    {
                        if ((bool)jsDriver.ExecuteScript("return jQuery.active == 0"))
                        {
                            //Console.WriteLine("Ajax done");
                            return;
                        }
                        //Console.WriteLine("Ajax not done : " + (timeOut - (i * 100)) + " ms left");
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    Console.WriteLine("This webdriver cannot execute Javascript");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static Func<IWebDriver, IWebElement> AttributeEquals(By locator, string attribute, string attValue)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return (element != null && element.GetAttribute(attribute) == attValue) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }
        public static Func<IWebDriver, IWebElement> AttributeEquals(IWebElement element, string attribute, string attValue)
        {
            return driver =>
            {
                return (element != null && element.GetAttribute(attribute) == attValue) ? element : null;
            };
        }

        public static Func<IWebDriver, IWebElement> AttributeNotEquals(By locator, string attribute, string attValue)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return (element != null && element.Displayed && element.GetAttribute(attribute) != attValue) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }
        public static Func<IWebDriver, IWebElement> AttributeNotEquals(IWebElement element, string attribute, string attValue)
        {
            return driver =>
            {
                return (element != null && element.Displayed && element.GetAttribute(attribute) != attValue) ? element : null;
            };
        }

        public static Func<IWebDriver, IWebElement> ElementIsClickable(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return (element != null && element.Displayed && element.Enabled) ? element : null;
                }
                catch (Exception)
                {
                    return null;
                }
            };
        }
        public static Func<IWebDriver, IWebElement> ElementIsClickable(IWebElement element)
        {

            return driver =>
            {
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }

        public static Func<IWebDriver, IWebElement> TextEquals(By locator, string expectedText)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return (element != null && element.Text == expectedText) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }
        public static Func<IWebDriver, IWebElement> TextEquals(IWebElement element, string expectedText)
        {
            return driver =>
            {
                return (element != null && element.Displayed && element.Text == expectedText) ? element : null;
            };
        }
    }
}
