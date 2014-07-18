using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

using RealstewTestingResources;

namespace RealstewTestCollection
{
    [TestClass]
    public class Masterpage_Tests
    {
        WebDriverWait wait;
        IWebDriver driver;

        public Masterpage_Tests()
        {
            //========================
            //Select driver here
            driver = new FirefoxDriver();
            //driver = new ChromeDriver();
            //driver = new InternetExplorerDriver();
            //========================

            driver.Navigate().GoToUrl("http://www.realstew1.realstew.com/");
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(2));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(7));

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        }
        [TestMethod, TestCategory("Masterpage")]
        public void LoginTest()
        {
            int attempts = 0;
            IWebElement userBar = wait.Until(CustomConditions.ElementIsClickable(UIMap.NavigationBar.UserBar));


        }
    }
}
