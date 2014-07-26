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
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        }

        [TestCleanup()]
        ~Masterpage_Tests()
        {
            driver.Quit();
            driver.Dispose();
        }
        [TestMethod, TestCategory("Masterpage")]
        public void LoginTest()
        {
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 5000);
            Assert.IsTrue(Masterpage.IsLoggedIn(driver));
        }
    
        [TestMethod, TestCategory("Masterpage")]
        public void Navigation_ContactsMenuItem()
        {
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 1000);
            wait.IgnoreExceptionTypes(typeof(TimeoutException));

            driver.FindElement(UIMap.NavigationBar.Contacts).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.OpenContactbook)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.LoadContactButton)) != null);
            driver.FindElement(UIMap.Contactbook.ContactbookClose).Click(); 
            
            driver.FindElement(UIMap.NavigationBar.Contacts).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.OpenLoadContact)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.LoadContact.InputField_Email)) != null);
            driver.FindElement(UIMap.Contactbook.ContactbookClose).Click();

            driver.FindElement(UIMap.NavigationBar.Contacts).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.OpenBulkLoadContacts)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.BulkLoadContact.ImportOptionDropDown)) != null);
            driver.FindElement(UIMap.Contactbook.ContactbookClose).Click();

        }
    
    }
}
