using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;


namespace RealstewTestingResources
{
    public static class Contactbook
    {
        public static void OpenContactbook(IWebDriver driver)
        {
            if (!Masterpage.IsLoggedIn(driver)) throw new Exception("User is not logged in when trying to access contact book");
            
            int attemptCounter = 0;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.IgnoreExceptionTypes(typeof(TimeoutException));

            do
            {
                if (attemptCounter > 3) throw new Exception("Contactbook Cannot be opened. Possibly due to a slow connection");
                attemptCounter++;
                wait.Until(CustomConditions.ElementIsClickable(UIMap.NavigationBar.Contacts)).Click();
                wait.Until(CustomConditions.ElementIsClickable(UIMap.NavigationBar.OpenContactbook)).Click();
                CustomConditions.WaitForAjax(driver, 5000);
            } while (wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.ContactbookElement)) == null);

        }
        public static void OpenLoadContact(IWebDriver driver)
        {
            if (!Masterpage.IsLoggedIn(driver)) throw new Exception("User is not logged in when trying to access contact book");

            int attemptCounter = 0;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.IgnoreExceptionTypes(typeof(TimeoutException));

            do
            {
                if (attemptCounter > 3) throw new Exception("Number of attempts to open LoadContact exceeded");
                attemptCounter++;
                wait.Until(CustomConditions.ElementIsClickable(UIMap.NavigationBar.Contacts)).Click();
                wait.Until(CustomConditions.ElementIsClickable(UIMap.NavigationBar.OpenLoadContact)).Click();
            } while (!IsLoadContactOpen(driver));
        }
        public static void OpenBulkLoadContacts(IWebDriver driver)
        {
            if (!Masterpage.IsLoggedIn(driver))
            {
                throw new Exception("User is not logged in when trying to access contact book");
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);

            wait.Until(CustomConditions.ElementIsClickable(UIMap.NavigationBar.Contacts)).Click();
            wait.Until(CustomConditions.ElementIsClickable(UIMap.NavigationBar.OpenBulkLoadContacts)).Click();

        }

        public static bool IsContactbookOpen(IWebDriver driver)
        {
            CustomConditions.WaitForAjax(driver, 1000);
            return (driver.FindElement(UIMap.Contactbook.ContactbookElement) != null) ? true : false;
        }
        public static bool IsLoadContactOpen(IWebDriver driver)
        {
            CustomConditions.WaitForAjax(driver, 1000);
            return (driver.FindElement(UIMap.Contactbook.LoadContact.InputField_Email) != null) ? true : false;
        }
        public static bool IsProfileOpen(IWebDriver driver)
        {
            CustomConditions.WaitForAjax(driver, 1000);
            return (driver.FindElement(UIMap.Contactbook.Profile.InputField_FirstName) != null) ? true : false;
        }
        public static bool AreTabsDisplayed(IWebDriver driver)
        {
            try
            {
                if (!Masterpage.IsLoggedIn(driver)) return false;

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.HorizontalTabClass));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        public static bool IsSearchbarDisplayed(IWebDriver driver)
        {
            try
            {
                if (!Masterpage.IsLoggedIn(driver)) return false;

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.Searchbar));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static void LoadUser(IWebDriver driver, UserInfo newUser)
        {
            OpenLoadContact(driver);
            CustomConditions.WaitForAjax(driver, 5000);


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);



            //Moved here, beause it updates the page and causes stale element exception
            if (newUser.email != null) wait.Until(CustomConditions.ElementIsClickable(UIMap.Contactbook.LoadContact.InputField_Email)).SendKeys(newUser.email + '\n');
            CustomConditions.WaitForAjax(driver, 5000);


            ReadOnlyCollection<IWebElement> fieldList = driver.FindElements(UIMap.Contactbook.LoadContact.TextInputClass); //find all the input field elements
            List<string> infoList = newUser.CompressFields();


            for (int i = 1; i < fieldList.Count; i++)
            {
                if (infoList[i] != null)
                {
                    fieldList[i].SendKeys(infoList[i] + "\n");
                }
            }

            List<IWebElement> menuList = driver.FindElements(UIMap.Contactbook.LoadContact.LocationDropDownClass).Concat(driver.FindElements(UIMap.Contactbook.LoadContact.GeneralDropDownClass)).ToList();
            List<int> valueList = newUser.CompressMenuItems();

            for (int i = 0; i < valueList.Count(); i++)
            {
                if (valueList[i] > 0)
                {
                    SelectElement select = new SelectElement(menuList[i]);
                    select.SelectByIndex(valueList[i]);
                }
            }

            wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.LoadContact.UpdateSubmitBtn)).Click();

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.LoadContact.Alert_UpdateSuccessfull));
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Could not verify if details were added properly");
            }
        }

        public static void RemoveContact(IWebDriver driver, UserInfo existingUser)
        {
            if (existingUser.email == null) throw new Exception("Cannot remove user without email. The email is a unique identifier");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);

            Navigate.Search.ByEmail(driver, existingUser.email);
            CustomConditions.WaitForAjax(driver, 5000);

            IWebElement container = wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.DirectoryListContainer));

            IWebElement checkBox = container.FindElements(By.TagName("input"))[0];

            while (!checkBox.Selected)
            {
                checkBox.Click();
            }

            driver.FindElement(UIMap.Contactbook.DeleteSelectedBtn).Click(); //Click the 'delete selected' button

            if (wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.DeleteSelectedMessage)).Text == "deleted!")
            {
                throw new Exception("Cannot verify user deletion");
            }
        }

        public static class Navigate
        {
            public static void ToTab(IWebDriver driver, string tab, bool autoOpen = true)
            {
                //========= Prerequisit Checking ==============
                int tryCount = 0;
                while (!AreTabsDisplayed(driver))
                {
                    if (!autoOpen || tryCount == 5) throw new Exception("Contactbook is not open and auto open is off. Cannot navigate to tab " + tab + " (Attempts: " + tryCount + ")");
                    tryCount++;
                    OpenLoadContact(driver);
                    CustomConditions.WaitForAjax(driver, 5000);
                }
                //============================================

                // ======== SetUp ============================
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                wait.Until(CustomConditions.ElementIsClickable(UIMap.Contactbook.Tab_Profile));
                //We make sure to open a tab first so that the other tabs (i.e. 'Documents' ) are also available. Then we wait for active ajax elements to finish
                //============================================

                //======= Function Body =====================

                ReadOnlyCollection<IWebElement> tabList;
                IWebElement result = null;
                By locator;

                switch (tab)
                {
                    case "Contact Info":
                        locator = UIMap.Contactbook.Tab_ContactInfo;
                        break;
                    case "Documents":
                        locator = UIMap.Contactbook.Tab_Documents;
                        break;
                    case "Notes":
                        locator = UIMap.Contactbook.Tab_Notes;
                        break;
                    case "Accounting":
                        locator = UIMap.Contactbook.Tab_Accounting;
                        break;
                    case "News":
                        locator = UIMap.Contactbook.Tab_News;
                        break;
                    case "Profile":
                        locator = UIMap.Contactbook.Tab_Profile;
                        break;
                    default:
                        throw new Exception("Tab identifier not recognized");
                }


                driver.FindElement(locator).Click();

                CustomConditions.WaitForAjax(driver, 5000);

                if (tab == "News") wait.Until(CustomConditions.TextEquals(UIMap.Contactbook.News.PageHeader, "NewsLetters"));
                else if (tab == "Profile") wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.Profile.InputField_FirstName));
                else
                {
                    wait.Until(CustomConditions.TextEquals(UIMap.Contactbook.SelectedTabClass, tab));
                }
            }

            public static void ToLetter(IWebDriver driver, string tab, bool autoOpen = true)
            {
                int tryCount = 0;
                tab = tab.ToUpper();

                if ((int)tab[0] < 65 || (int)tab[0] > 90) throw new Exception("Invalid tab input");

                while (!AreTabsDisplayed(driver))
                {
                    if (!autoOpen || tryCount == 5) throw new Exception("Contactbook is not open and auto open is off. Cannot navigate to tab " + tab + " (Attempts: " + tryCount + ")");
                    tryCount++;
                    OpenLoadContact(driver);
                    CustomConditions.WaitForAjax(driver, 5000);
                }
                // ======== SetUp ============================
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                wait.Until(CustomConditions.ElementIsClickable(UIMap.Contactbook.Tab_Profile));
                //============================================

                //======= Function Body =====================
                string cssPath = "#crmAlpha > div:nth-child(" + ((int)tab[0] - 64) + ")";

                wait.Until(CustomConditions.ElementIsClickable(By.CssSelector(cssPath))).Click();

                CustomConditions.WaitForAjax(driver, 5000);
                wait.Until(CustomConditions.TextEquals(UIMap.Contactbook.IndexPageHeader, "Index - " + tab));
            }
            public static class Search
            {
                public static ReadOnlyCollection<IWebElement> ByName(IWebDriver driver, string lastName, bool autoOpen = true)
                {

                    //========= Prerequisit Checking ==============
                    if (lastName == null) throw new Exception("Tried to navigate to user without providing a last name");

                    int tryCount = 0;
                    while (!IsContactbookOpen(driver))
                    {
                        if (!autoOpen || tryCount == 3) throw new Exception("Contactbook is not open and auto open is off. Cannot navigate to user: " + lastName + " (Attempts: " + tryCount + ")");
                        tryCount++;
                        OpenContactbook(driver);
                    }
                    //============================================

                    // ======== SetUp ============================
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                    wait.PollingInterval = TimeSpan.FromMilliseconds(100);


                    wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.DirectoryFilterLetterClass)).Click(); //We must wait for the contact book to open, then we select the 'A' tab
                    CustomConditions.WaitForAjax(driver, 3000);
                    //We make sure to open a tab first so that the other tabs (i.e. 'Documents' ) are also available. Then we wait for active ajax elements to finish
                    //============================================

                    //======= Function Body ======================


                    IWebElement searchBox = wait.Until(CustomConditions.ElementIsClickable(UIMap.Contactbook.Searchbar));
                    while (searchBox.GetAttribute("value") != "")
                    {
                        searchBox.Clear();
                    }

                    searchBox.SendKeys(lastName);
                    CustomConditions.WaitForAjax(driver, 5000);

                    IWebElement resultContainer = wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.DirectoryListContainer));

                    if (resultContainer.Text != "no contacts found.")
                    {
                        ReadOnlyCollection<IWebElement> resultList = resultContainer.FindElement(By.TagName("div")).FindElements(By.TagName("div"));
                        if (!resultList[0].FindElement(By.TagName("div")).Text.Contains(lastName))
                        {
                            throw new Exception("Failed to verify ToUser navigation");
                        }

                        return resultList;
                    }
                    else throw new Exception("No results found");
                }
                public static void ByEmail(IWebDriver driver, string email, bool autoOpen = true)
                {

                    //========= Prerequisit Checking ==============
                    if (email == null) throw new Exception("Tried to navigate to user without providing an email");

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                    wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                    while (!IsContactbookOpen(driver) || !IsSearchbarDisplayed(driver))
                    {
                        if (!autoOpen) throw new Exception("Contactbook is not open and auto open is off. Cannot navigate to user: " + email + ")");
                        Navigate.ToLetter(driver, "A");
                    }
                    //============================================
                    //======= Function Body ======================


                    IWebElement searchBox = wait.Until(CustomConditions.ElementIsClickable(UIMap.Contactbook.Searchbar));
                    while (searchBox.GetAttribute("value") != "")
                    {
                        searchBox.Clear();
                    }

                    searchBox.SendKeys(email);

                    CustomConditions.WaitForAjax(driver, 5000);

                    IWebElement resultContainer = wait.Until(ExpectedConditions.ElementIsVisible(UIMap.Contactbook.DirectoryListContainer));

                    if (resultContainer.Text != "no contacts found.")
                    {
                        IWebElement firstResult = wait.Until(CustomConditions.ElementIsClickable(By.CssSelector("#crmDirectoryList > div:nth-child(1) > div > table > tbody > tr > td:nth-child(3)")));

                        if (!firstResult.Text.Contains(email))
                        {
                            throw new Exception("Failed to verify ToUser navigation");
                        }
                    }
                }
            }
        }
        public class UserInfo
        {
            //For drop down menus, we use the index of the value rather than the text. The reson for this is that it makes it much easier to write a recursive test with different values
            public string email = null;
            public string firstName = null;
            public string surname = null;
            public string businessName = null;
            public string contactPerson = null;

            public int category = 0; //Select index of value desired. 'Select category' is 0, 'Accomodation' is 1...
            public int subCategory = 0; //Same as category
            public int country = 0;
            public int province = 0;
            public int city = 0;
            public int suburb = 0;

            public string address1 = null;
            public string address2 = null;
            public string address3 = null;
            public string address4 = null;

            public string weblet = null;

            public string telIntCode = null;
            public string areaCode = null;

            public string telNumber = null;
            public string mobileNumber = null;

            public int DOB_Day = 0;
            public int DOB_Month = 0;
            public int DOB_Year = 0;

            public int gender = 0;

            public string partnerName = null;
            public string partnerEmail = null;
            public string partnerWeblet = null;
            public string partnerMobile = null;

            public UserInfo() { }

            public List<string> CompressFields()
            {
                //Compresses all the values for input fields (i.e. name, phone number, weblet) into a list. 
                //Note, this function excludes all drop down menu items. 
                List<string> resultList = new List<string>{email,firstName,surname,businessName,contactPerson,address1,address2,address3,address4,
                                                       weblet,telIntCode,areaCode,telNumber, mobileNumber, partnerName, partnerEmail, partnerWeblet, partnerMobile};
                return resultList;
            }

            public List<int> CompressMenuItems()
            {
                //Functionally similar to CompressFields(), but deals with drop down menu index values
                List<int> resultList = new List<int>{category,subCategory,country,province,city,
                                                       suburb,DOB_Year, DOB_Month, DOB_Year, gender};
                return resultList;

            }
        }
    }
}
