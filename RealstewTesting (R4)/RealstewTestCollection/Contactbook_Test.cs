using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Summary description for Contactbook_Test
    /// </summary>
    [TestClass]
    public class Contactbook_Test
    {
        WebDriverWait wait;
        IWebDriver driver;

        [TestInitialize()]
        public void ContactbookTest_Init()
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

        [TestCleanup()]
        ~Contactbook_Test()
        {
            driver.Quit();
            driver.Dispose();
        }

        [TestMethod, TestCategory("Contactbook")]
        public void OpenContactbook()
        {
            Masterpage.Login(driver);
            Contactbook.OpenContactbook(driver);
            CustomConditions.WaitForAjax(driver, 3000);
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.ContactbookElement)) != null);
        }
    }
}
