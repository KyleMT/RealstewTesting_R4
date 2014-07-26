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
    public static class UIMap
    {
        public static class NavigationBar
        {
            public static By Contacts = By.Id("d_contacts");
            public static By OpenContactbook = By.CssSelector("#bar_contact_list_container > div:nth-child(3)");
            public static By OpenLoadContact = By.CssSelector("#bar_contact_list_container > div:nth-child(4)");
            public static By OpenBulkLoadContacts = By.CssSelector("#bar_contact_list_container > div:nth-child(5)");
            public static By UserBar = By.Id("bar_user");
        }

        public static class LoginForm
        {
            public static By LoginBody = By.Id("rsLogin_Body");
            public static By InputField_Email = By.Id("tbLoginEmail");
            public static By InputField_Password = By.Id("tbLoginPassword");
            public static By SubmitBtn = By.Id("btnLoginSubmit");

            public static By Alert_LoginError = By.Id("logdetailmessage_body");

        }

        public static class Contactbook
        {

            public static By Tab_Documents = By.Id("crmtab_documents");
            public static By Tab_Profile = By.CssSelector("#pageerr > table > tbody > tr > td:nth-child(1) > div:nth-child(1) > div.crmTabs > div:nth-child(7)");
            public static By Tab_Notes = By.Id("crmtab_notes");
            public static By Tab_Accounting = By.Id("crmtab_accounting");
            public static By Tab_News = By.Id("crm_tab_newsletter");
            public static By Tab_ContactInfo = By.Id("crmtab_details");

            public static By HorizontalTabClass = By.ClassName("crmTabs");
            public static By TabClass = By.ClassName("crm_css_tab");

            public static By DirectoryFilterCollection = By.Id("crmAlpha");
            public static By DirectoryFilterLetterClass = By.ClassName("crmAlpha_alph");

            public static By DirectoryListContainer = By.Id("crmDirectoryList");
            public static By DeleteSelectedBtn = By.CssSelector("#crmBook > div.crmBook > div:nth-child(1) > div > table > tbody > tr > td:nth-child(4) > img");
            public static By DeleteSelectedMessage = By.Id("deletemsg");

            public static By IndexPageHeader = By.CssSelector("#crmBook > div.crmBook > div:nth-child(1) > div > table > tbody > tr > td:nth-child(1)");

            public static By SelectedTabClass = By.ClassName("crm_css_tab_selected");

            public static By Searchbar = By.Id("tbSearch");

            public static By ContactbookElement = By.Id("pageerr");

            public static By ContactbookClose = By.CssSelector("#pageerr > table > tbody > tr > td:nth-child(1) > div:nth-child(1) > div:nth-child(2)");

            public static By LoadContactButton = By.CssSelector("#crmBook > div > div:nth-child(1) > div:nth-child(1) > div");
            public static class LoadContact
            {
                public static By TextInputClass = By.ClassName("crmField");
                public static By LocationDropDownClass = By.ClassName("crmDropDownBus");
                public static By GeneralDropDownClass = By.ClassName("crmDropDown");

                public static By InputField_FirstName = By.Id("tbFirstName");
                public static By InputField_Email = By.Id("tbNewEmail");

                public static By UpdateSubmitBtn = By.CssSelector("#crmBook > div.crmBook > div:nth-child(2) > div:nth-child(3) > img");

                public static By Alert_UpdateSuccessfull = By.Id("cBookDetailsAlert");

            }

            public static class BulkLoadContact
            {
                public static By ImportOptionDropDown = By.Id("dlimportType");
            }
            
            public static class News
            {
                public static By PageHeader = By.CssSelector("#crmBook > div.crmNewsletter > div > div:nth-child(1)");
            }
            public static class Profile
            {
                public static By InputField_FirstName = By.Id("firstname");

            }
        
        
        }
    }
}
