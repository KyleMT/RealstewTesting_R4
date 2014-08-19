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
            wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

            driver.FindElement(UIMap.NavigationBar.Contacts).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.ContactsMenu.OpenContactbook)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.LoadContactButton)) != null);
            driver.FindElement(UIMap.Contactbook.ContactbookClose).Click();

            driver.FindElement(UIMap.NavigationBar.Contacts).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.ContactsMenu.OpenLoadContact)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.LoadContact.InputField_Email)) != null);
            driver.FindElement(UIMap.Contactbook.ContactbookClose).Click();

            driver.FindElement(UIMap.NavigationBar.Contacts).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.ContactsMenu.OpenBulkLoadContacts)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.BulkLoadContact.ImportOptionDropDown)) != null);
            driver.FindElement(UIMap.Contactbook.ContactbookClose).Click();
        }

        [TestMethod, TestCategory("Masterpage")]
        public void Navigation_ChatMenuItem()
        {
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 1000);
            wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

            driver.FindElement(UIMap.NavigationBar.Chat).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.ChatMenu.ChatReport));

            CustomConditions.WaitForAjax(driver, 1000);
            if (driver.FindElement(By.Id("chatlist")).FindElements(By.TagName("div")).Count > 0)
            {
                IWebElement subMenuItem = driver.FindElement(By.Id("chatlist")).FindElements(By.TagName("div"))[1];
                subMenuItem = subMenuItem.FindElements(By.TagName("div"))[1];
                subMenuItem = subMenuItem.FindElement(By.TagName("div"));
                if (subMenuItem != null)
                {
                    subMenuItem.Click();
                    Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Chat.ChatWindowClose)) != null);
                    driver.FindElement(UIMap.NavigationBar.Chat).Click();
                }
            }

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.ChatMenu.ChatReport)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Chat.ChatReport.ChatReportWindow)) != null);
            driver.FindElement(UIMap.Chat.ChatReport.ChatReportWindowClose).Click();


            driver.FindElement(UIMap.NavigationBar.Chat).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.ChatMenu.QuickChat)).Click();
            IWebElement label = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#form1 > div:nth-child(4) > div:nth-child(7) > div > div:nth-child(3)")));
            Assert.IsTrue(label.Text == "Load and Open Chat");

            driver.FindElement(UIMap.NavigationBar.Chat).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.ChatMenu.MultiChat)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Chat.MultiChat.MultiChatPage)) != null);

        }

        [TestMethod, TestCategory("Masterpage")]
        public void Navigation_UserGroupMenuItem()
        {
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 1000);
            wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

            driver.FindElement(UIMap.NavigationBar.UserGroups).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserGroupsMenu.CreateUserGroups));
            if (driver.FindElement(By.Id("usergrouplist")).FindElements(By.TagName("div")).Count > 0)
            {
                IWebElement subMenuItem = driver.FindElement(By.Id("usergrouplist")).FindElements(By.TagName("div"))[1];
                subMenuItem = subMenuItem.FindElements(By.TagName("div"))[0];
                subMenuItem = subMenuItem.FindElement(By.TagName("a"));
                if (subMenuItem != null)
                {
                    subMenuItem.Click();
                    Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.UserGroup.UserGroupHeader)) != null);
                    driver.FindElement(UIMap.NavigationBar.UserGroups).Click();
                }
            }

            driver.FindElement(UIMap.NavigationBar.UserGroups).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserGroupsMenu.CreateUserGroups)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.UserGroup.CreateUserGroup.CreateNewUserGroup)) != null);

            driver.FindElement(UIMap.NavigationBar.UserGroups).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserGroupsMenu.JoinUserGroup)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.UserGroup.JoinUserGroup.JoinExistingUserGroup)) != null);

        }

        [TestMethod, TestCategory("Masterpage")]
        public void Navigation_VideoChatMenuItem()
        {
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 1000);
            wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.VideoChat)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#d_videochat_list > div:nth-child(3) > img"))) != null);
        }

        [TestMethod, TestCategory("Masterpage")]
        public void Navigation_AlertsMenuItem()
        {
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 1000);
            wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.Alerts)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#d_alert_list > div:nth-child(3) > div:nth-child(1)"))) != null);
        }

        [TestMethod, TestCategory("Masterpage")]
        public void Navigation_SearchMenuItem()
        {
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 1000);
            wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.Search)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.SearchPage.SearchBar)) != null);
        }

        [TestMethod, TestCategory("Masterpage")]
        public void Navigation_UserControlMenuItem()
        {
            // NEED TO SLOW THIS DOWN
            Masterpage.Login(driver);
            CustomConditions.WaitForAjax(driver, 1000);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.WatchList)) != null);
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();

            CustomConditions.WaitForAjax(driver, 2000);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControl)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.WatchList)) != null);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.WatchList)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.WatchList.Body)) != null);
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.WatchList.CloseButton)).Click();

            CustomConditions.WaitForAjax(driver, 3000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);


            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.BlogManagement)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.BlogManagementMenu.BlogsIFollow)) != null);
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.BlogManagementMenu.BlogsIFollow)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.SubscribedBlogList.Body)) != null);
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.SubscribedBlogList.CloseButton)).Click();

            CustomConditions.WaitForAjax(driver, 2000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);

            

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.ApplicationManagement)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.ApplicationManagementMenu.AppStore)) != null);
              wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.ApplicationManagementMenu.AppSubscriptions)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.SubscribedAppList.Header)) != null);

            CustomConditions.WaitForAjax(driver, 2000);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControl)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.ApplicationManagement)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.ApplicationManagementMenu.AppStore)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.FloatMenu.MenuElement)) != null);
            driver.FindElement(UIMap.FloatMenu.CloseButtom).Click();

            CustomConditions.WaitForAjax(driver, 2000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagement)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagementMenu.RealFinancial)) != null);
             wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagementMenu.RealFinancial)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.RealFinacial.CoinIcon)) != null);

            CustomConditions.WaitForAjax(driver, 2000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);


            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagement)).Click();
              wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagementMenu.Statement)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.StatementForm.Container)) != null);

            CustomConditions.WaitForAjax(driver, 2000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagement)).Click();
             wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagementMenu.BuyCredits)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.TopUpCreditsForm.Container)) != null);
              wait.Until(ExpectedConditions.ElementIsVisible(UIMap.TopUpCreditsForm.CloseButtom)).Click();

            CustomConditions.WaitForAjax(driver, 2000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);


            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagement)).Click();
              wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.MoneyManagementMenu.StewVille)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.StewVille.Header)) != null);

            CustomConditions.WaitForAjax(driver, 2000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.Profile)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.Profile.InputField_FirstName)) != null);

            CustomConditions.WaitForAjax(driver, 2000);

            do
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserBar)).Click();
                CustomConditions.WaitForAjax(driver, 2000);
            } while (driver.FindElement(UIMap.NavigationBar.UserControlMenu.WatchList).Displayed == false);

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.NavigationBar.UserControlMenu.AccountSettings)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(UIMap.AccountSettings.Header)) != null);


        }



    }
}
