using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [TestMethod, TestCategory("Contactbook")]
        public void OpenLoadcontact()
        {
            Masterpage.Login(driver);
            Contactbook.OpenLoadContact(driver);
            CustomConditions.WaitForAjax(driver, 3000);
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.LoadContact.InputField_Email)) != null);
        }
        [TestMethod, TestCategory("Contactbook")]
        public void NavigationTest_ToTab()
        {
            Masterpage.Login(driver);
            List<string> tabList = new List<string> { "Documents", "Notes", "Accounting" };

            for (int i = 0; i < tabList.Count; i++)
            {
                Contactbook.Navigate.ToTab(driver, tabList[i]);
                Assert.IsTrue(driver.FindElement(By.ClassName("crm_css_tab_selected")).GetAttribute("id") == "crmtab_" + (tabList[i].ToLower()));
                CustomConditions.WaitForAjax(driver, 5000);
            }
        }
        [TestMethod, TestCategory("Contactbook")]
        public void NavigationTest_ToUser_Name()
        {
            string user = "Taylor";

            Masterpage.Login(driver);
            Contactbook.OpenContactbook(driver);

            ReadOnlyCollection<IWebElement> resultList = Contactbook.Navigate.Search.ByName(driver, user);

            bool userFound = false;
            foreach (IWebElement em in resultList)
            {
                if (em.FindElement(By.TagName("div")).Text.Contains(user))
                {
                    userFound = true; 
                    break;
                };                
            }
           
            Assert.IsTrue(userFound);
        }
        [TestMethod, TestCategory("Contactbook")]
        public void NavigationTest_ToUser_Email()
        {
            string email = "clive.taylor@xtra.co.nz";

            Masterpage.Login(driver);
            Contactbook.OpenContactbook(driver);

            Contactbook.Navigate.Search.ByEmail(driver, email);

            Assert.IsTrue(driver.FindElement((By.CssSelector("#crmDirectoryList > div:nth-child(1) > div > table > tbody > tr > td:nth-child(3)"))).Text.Contains(email));
        }
        [TestMethod, TestCategory("Contactbook")]
        public void LoadContact()
        {

            Masterpage.Login(driver);

            Contactbook.UserInfo newUser = new Contactbook.UserInfo();

            newUser.email = "test@test.com";
            newUser.DOB_Day = 1;
            newUser.DOB_Month = 1;
            newUser.DOB_Year = 1;

            Contactbook.LoadUser(driver, newUser);
        }
        [TestMethod, TestCategory("Contactbook")]
        public void RemoverContact()
        {

            Masterpage.Login(driver);

            Contactbook.UserInfo newUser = new Contactbook.UserInfo();

            newUser.email = "test@test.com";
            newUser.DOB_Day = 1;
            newUser.DOB_Month = 1;
            newUser.DOB_Year = 1;

            Contactbook.RemoveContact(driver, newUser);

            try
            {
                Contactbook.Navigate.Search.ByEmail(driver, newUser.email);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "No results found");
            }
        }
        [TestMethod, TestCategory("InputValidation")]
        public void InputFields_LoadContact()
        {

            Masterpage.Login(driver);

            Contactbook.OpenLoadContact(driver);

            string numericKeys = "1234567890";
            string alphaKeys = "qwertyuiopasdfghjklzxcvbnm";
            string alphaKeysCap = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string specialChar = "!@#$%&\"':;<,>.?/\\~` ";
            string arithmeticChar = "/*-+()";

            List<string> numericRestriction = new List<string> { "tbIntCode", "tbAreaCode", "tbTel", "tbMobile", "tbPartnerMobile" };
            List<string> alphaNumeralRestriction = new List<string> { "tbFirstName", "tbSurname", "tbPartnerName", "tbAdd1", "tbAdd2", "tbAdd3", "tbAdd4" };

            ReadOnlyCollection<IWebElement> elementList = driver.FindElements(By.ClassName("crmField"));

            List<string> failedElementsList = new List<string>();

            foreach (IWebElement element in elementList)
            {
                string input = null;

                if (numericRestriction.Contains(element.GetAttribute("id")))
                {
                    input = alphaKeys + alphaKeysCap + specialChar + arithmeticChar;
                }
                else if (alphaNumeralRestriction.Contains(element.GetAttribute("id")))
                {
                    input = numericKeys + arithmeticChar;
                }

                if (input != null)
                {
                    foreach (char c in input)
                    {
                        wait.Until(CustomConditions.ElementIsClickable(element)).Clear();
                        element.SendKeys("" + c);
                        if (element.GetAttribute("value") == "" + c)
                            if (failedElementsList.Contains(element.GetAttribute("id")) == false) failedElementsList.Add(element.GetAttribute("id"));
                    }
                }
            }

            Console.WriteLine("Failed field ids :");
            foreach (string item in failedElementsList)
            {
                Console.WriteLine(item);
            }

            Assert.IsTrue(failedElementsList.Count == 0);
        }
        [TestMethod, TestCategory("Contactbook")]
        public void DocumentTab()
        {
            Masterpage.Login(driver);
            Contactbook.Navigate.Search.ByEmail(driver, "clive.taylor@xtra.co.nz");
            CustomConditions.WaitForAjax(driver, 5000);
            wait.Until(CustomConditions.ElementIsClickable(By.CssSelector("#crmDirectoryList > div:nth-child(1) > div > table > tbody > tr > td:nth-child(2) > div"))).Click();
            wait.Until(CustomConditions.ElementIsClickable(By.Id("tbFirstName")));
            Contactbook.Navigate.ToTab(driver, "Documents");
        }
        [TestMethod, TestCategory("Contactbook")]
        public void Navigation_ToLetter()
        {
            Masterpage.Login(driver);

            for (int i = (int)'A'; i <= 90; i++)
            {
                string tab = "" + ((char)i);
                Contactbook.Navigate.ToLetter(driver, tab); 
                CustomConditions.WaitForAjax(driver, 5000);
                Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#crmBook > div.crmBook > div:nth-child(1) > div > table > tbody > tr > td:nth-child(1)"))).Text == "Index - " + tab);
            }

        }
    }
}
