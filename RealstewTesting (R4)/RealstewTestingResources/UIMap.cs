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
            public static By UserBar = By.Id("bar_user");
            public static By Chat = By.Id("bar_chat");
            public static By UserGroups = By.Id("bar_usergroup");
            public static By VideoChat = By.Id("d_videochat");
            public static By Alerts = By.Id("bar_alerts");
            public static By Search = By.Id("bar_search");
            public static By UserControl = By.Id("d_userControl");

            public static class ContactsMenu
            {
                public static By OpenContactbook = By.CssSelector("#bar_contact_list_container > div:nth-child(3)");
                public static By OpenLoadContact = By.CssSelector("#bar_contact_list_container > div:nth-child(4)");
                public static By OpenBulkLoadContacts = By.CssSelector("#bar_contact_list_container > div:nth-child(5)");
            }
            public static class ChatMenu
            {
                public static By ChatReport = By.CssSelector("div.dropdownbuttons:nth-child(2)");
                public static By QuickChat = By.CssSelector("div.dropdownbuttons:nth-child(3)");
                public static By MultiChat = By.CssSelector("div.dropdownbuttons:nth-child(4)");
            }
            public static class UserGroupsMenu
            {
                public static By CreateUserGroups = By.CssSelector("div.dropdownbuttons:nth-child(2)");
                public static By JoinUserGroup = By.CssSelector("div.dropdownbuttons:nth-child(3)");
            }
            public static class UserControlMenu
            {
                public static By WatchList = By.Id("usr_bar_watch");
                public static By BlogManagement = By.Id("usr_bar_blog");
                public static By ApplicationManagement = By.Id("usr_bar_app");
                public static By MoneyManagement = By.Id("usr_bar_money");
                public static By Profile = By.Id("usr_bar_profile");
                public static By AccountSettings = By.Id("usr_account_profile");

                public static class BlogManagementMenu
                {
                    public static By BlogsIFollow = By.CssSelector("#listUser_1_blog > div.barlistitem");
                }
                public static class ApplicationManagementMenu
                {
                    public static By AppSubscriptions = By.CssSelector("#listUser_1_app > div:nth-child(1)");
                    public static By AppStore = By.CssSelector("#listUser_1_app > div:nth-child(2) > div:nth-child(1)");
                }

                public static class MoneyManagementMenu
                {
                    public static By RealFinancial = By.CssSelector("#listUser_1_money > div:nth-child(1)");
                    public static By Statement = By.CssSelector("#listUser_1_money > div:nth-child(2)");
                    public static By BuyCredits = By.CssSelector("#listUser_1_money > div:nth-child(3)");
                    public static By StewVille = By.CssSelector("#listUser_1_money > div:nth-child(4)");
                }


            }
        }
        public static class AccountSettings
        {
            public static By Header = By.Id("accset_Head_txt");
        }
        public static class TopUpCreditsForm
        {
            public static By Container = By.Id("tuc_Container");
            public static By CloseButtom = By.Id("buycredits_close");
        }
        public static class StewVille
        {
            public static By Header = By.Id("stewvilleHdr");
        }
        public static class RealFinacial
        {
            public static By CoinIcon = By.Id("coin");
        }
        public static class StatementForm
        {
            public static By Container = By.Id("statem_Container");
        }
        public static class FloatMenu
        {
            public static By MenuElement = By.Id("FloaterMenu");
            public static By CloseButtom = By.CssSelector("#FloaterMenu > div:nth-child(1)");
        }

        public static class SearchPage
        {
            public static By SearchBar = By.Id("SiteHeader_SiteTabs_BarOfSearch_mainSearchControl");
        }
        public static class LoginForm
        {
            public static By LoginBody = By.Id("rsLogin_Body");
            public static By InputField_Email = By.Id("tbLoginEmail");
            public static By InputField_Password = By.Id("tbLoginPassword");
            public static By SubmitBtn = By.Id("btnLoginSubmit");
            public static By Alert_LoginError = By.Id("logdetailmessage_body");
        }

        public static class Chat
        {
            public static By ChatWindowBody = By.CssSelector("#form1 > div:nth-child(7)");
            public static By ChatWindowClose = By.Id("dCloseChat");
            public static class MultiChat
            {
                public static By MultiChatPage = By.Id("chatdetails");
            }
            public static class ChatReport
            {
                public static By ChatReportWindow = By.Id("chatLog_container");
                public static By ChatReportWindowClose = By.CssSelector("#chatLog_container > div.rs_Nav_bar > div.rs_Nav_bar_close > img");
            }
        }
        public static class UserGroup
        {
            public static By UserGroupHeader = By.Id("ug_hdr");
            public static By ContentElement = By.Id("usergroup");

            public static class CreateUserGroup
            {
                public static By CreateNewUserGroup = By.Id("createexistingusergroup");
            }
            public static class JoinUserGroup
            {
                public static By JoinExistingUserGroup = By.Id("joinexistingusergroup");
            }
        }
        public static class WatchList
        {
            public static By Body = By.Id("watchlist_body");
            public static By CloseButton = By.CssSelector(".rs_Nav_bar_close > img:nth-child(1)");
        }
        public static class SubscribedBlogList
        {
            public static By Body = By.Id("dynamiclike");
            public static By CloseButton = By.CssSelector(".contacts > div:nth-child(2)");
        }

        public static class SubscribedAppList
        {
            public static By Header = By.Id("appregun_head");
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
