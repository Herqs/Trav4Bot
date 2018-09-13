using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Travian_bot_v1
{
    public class TravianBrowser
    {
        private string login, pass;
        public string status;
        IWebDriver chrome;
        public string url;
        private string urld1;
        private string urld2;

        public TravianBrowser(ChromeDriverService service, ChromeOptions options, string login, string pass, string url)
        {
            chrome = new ChromeDriver(service, options);
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

            this.url = url;
            urld1 = url + "dorf1.php";
            urld2 = url + "dorf2.php";

            this.login = login;
            this.pass = pass;
            string[] details = new string[] { login, pass };
        }

        public void LoginTravian()
        {
            chrome.Navigate().GoToUrl(url);
            Thread.Sleep(2000);
            try
            {
                var LoginField = chrome.FindElement(By.XPath("//*[@id=\"content\"]/div[1]/div[1]/form/table/tbody/tr[1]/td[2]/input"));
                var PassField = chrome.FindElement(By.XPath("//*[@id=\"content\"]/div[1]/div[1]/form/table/tbody/tr[2]/td[2]/input"));
                var LoginBtn = chrome.FindElement(By.XPath("//*[@id=\"s1\"]/div/div[2]"));

                LoginField.SendKeys(login);
                PassField.SendKeys(pass);

                Cookie LangCookie = chrome.Manage().Cookies.GetCookieNamed("travian-language");

                try
                {
                    if (LangCookie.Value != "en-US")
                    {
                        MessageBox.Show("trav lang is not en_US");
                        chrome.Manage().Cookies.DeleteCookie(LangCookie);
                        Cookie EngCookie = new Cookie("travian-language", "en-US");
                        chrome.Manage().Cookies.AddCookie(EngCookie);
                    }
                }
                catch
                {
                    MessageBox.Show("Failed");
                    Cookie EngCookie = new Cookie("travian-language", "en-US");
                    chrome.Manage().Cookies.AddCookie(EngCookie);
                }
                if (!chrome.FindElement(By.Id("lowRes")).Selected)
                    chrome.FindElement(By.Id("lowRes")).Click();

                LoginBtn.Click();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loging in");
            }
        }

        public void Testt()
        {
            var tortrn = new List<Cookie>();
            foreach (Cookie cook in chrome.Manage().Cookies.AllCookies)
            {
                tortrn.Add(cook);
                var ddd = cook;
                MessageBox.Show(ddd.Domain + "\n" + ddd.Expiry + "\n" + ddd.Path + "\n" + ddd.Value + "\n" + ddd.Secure + "\n" + ddd.Name);
            }
        }
        public Cookie get1cook()
        {
            var tortrn = new List<Cookie>();
            foreach (Cookie cook in chrome.Manage().Cookies.AllCookies)
            {
                return cook;
            }
            MessageBox.Show("NO COOKY FOUND");
            return new Cookie("asd", "eee");
        }

        public List<Building> RefreshBuildingAreas()
        {
            List<Building> BuildingList = new List<Building>();
            if (chrome.Url != urld2)
                chrome.Navigate().GoToUrl(urld2);
            var buildings = chrome.FindElement(By.Id("village_map"));
            var building_list = buildings.FindElements(By.ClassName("buildingSlot"));
            foreach(IWebElement building in building_list)
            {
                try
                {
                    string[] temp = building.GetAttribute("class").Split(' ');
                    string Name = Id_To_Build(new String(temp[2].Where(Char.IsDigit).ToArray())); // CIA YRA ID PASTATO
                    string BuildUrl = url + "build.php?id=" + (new String(temp[3].Where(Char.IsDigit).ToArray()));

                    BuildingList.Add(new Building(Name, BuildUrl, Int32.Parse(building.FindElement(By.ClassName("labelLayer")).Text),Name_Is_Resource(Name)));
                }
                catch { }
            }
            return BuildingList;
        }

        public bool Name_Is_Resource(string name)
        {
            if (name.Contains("Woodcutter"))
                return true;
            if (name.Contains("Cropland"))
                return true;
            if (name.Contains("Iron Mine"))
                return true;
            if (name.Contains("Clay Pit"))
                return true;
            
            return false;
        }

        string Id_To_Build(string id)
        {
            switch (id)
            {
                case "5":
                    return "Sawmill	";
                case "6":
                    return "Brickyard";
                case "7":
                    return "Iron Foundry";
                case "8":
                    return "Grain Mill";
                case "9":
                    return "Bakery";
                case "10":
                    return "Warehouse";
                case "11":
                    return "Granary";
                case "12":
                    return "ERROR XD 12 case";
                case "13":
                    return "Smithy";
                case "14":
                    return "Tournament Square";
                case "15":
                    return "Main Building";
                case "16":
                    return "Rally Point";
                case "17":
                    return "Marketplace";
                case "18":
                    return "Embassy";
                case "19":
                    return "Barracks";
                case "20":
                    return "Stable";
                case "21":
                    return "Workshop";
                case "22":
                    return "Academy";
                case "23":
                    return "Cranny";
                case "24":
                    return "Town Hall";
                case "25":
                    return "Residence";
                case "26":
                    return "Palace";
                case "27":
                    return "Treasury";
                case "28":
                    return "Trade Office";
                case "29":
                    return "Great Barracks";
                case "30":
                    return "Great Stable";
                case "31":
                    return "City Wall";
                case "32":
                    return "Earth Wall";
                case "33":
                    return "Palisade";
                case "34":
                    return "Stonemason's Lodge";
                case "35":
                    return "Brewery";
                case "36":
                    return "Trapper";
                case "37":
                    return "Hero's Mansion";
                case "38":
                    return "Great Warehouse";
                case "39":
                    return "Great Granary";
                case "40":
                    return "Wonder of the World";
                case "41":
                    return "Horse Drinking Trough";
                default:
                    throw new Exception("ID NOT FOUND IN LIST OF ID's");
            }
        }

        public void Navigate(string element)
        {
            try { chrome.Navigate().GoToUrl(element); }
            catch { MessageBox.Show("Error resolving url: " + element); }
        }

        public void dorf1()
        {
            try { chrome.Navigate().GoToUrl(urld1); }
            catch { MessageBox.Show("Error resolving url: " + urld1); }
        }

        public void dorf2()
        {
            try { chrome.Navigate().GoToUrl(urld1); }
            catch { MessageBox.Show("Error resolving url: " + urld1); }
        }
        
        public string Is_under_attack()
        {
            try
            {
                chrome.FindElement(By.ClassName("att1"));
                return "###";
            }
            catch
            {
                return "";
            }
        }

        public List<Building> RefreshResourceAreas()
        {
            try
            {
                var dorf1 = chrome.FindElement(By.Id("rx"));
            }
            catch
            {
                Navigate(urld1);
            }

            List<Building> ResourceFields = new List<Building>();

            var map_element = chrome.FindElement(By.Id("rx")).FindElements(By.TagName("area"));

            foreach(IWebElement element in map_element)
            {
                string[] temp = element.GetAttribute("alt").Split(' ');
                try
                {
                    int level = Int16.Parse(new String(element.GetAttribute("alt").Where(Char.IsDigit).ToArray()));
                    Building Building = new Building(temp[0], element.GetAttribute("href"), level, true);
                    ResourceFields.Add(Building);
                }
                catch
                { }
            }
            return ResourceFields;
        }

        public List<TravianDoingTaskBuild> RefreshBuildTimes(int i)
        {
            List<TravianDoingTaskBuild> List = new List<TravianDoingTaskBuild>();
            try
            {
                var BuildBox = chrome.FindElement(By.ClassName("buildingList"));
                var ListBox = BuildBox.FindElement(By.TagName("ul"));
                var ListItems = ListBox.FindElements(By.TagName("li"));
                foreach(IWebElement element in ListItems)
                {

                    var lvl = Convert.ToInt16(new String((element.FindElement(By.ClassName("lvl")).Text).Where(Char.IsDigit).ToArray()));
                    var name = element.FindElement(By.ClassName("name")).Text;
                    MessageBox.Show(Name_Is_Resource(name).ToString());
                    string[] timestr = element.FindElement(By.ClassName("timer")).Text.Split(':');
                    TimeSpan time = new TimeSpan(Convert.ToInt16(timestr[0]), Convert.ToInt16(timestr[1]), Convert.ToInt16(timestr[2]));
                    List.Add(new TravianDoingTaskBuild(time, lvl, i, name, Name_Is_Resource(name), ""));
                }
                return List;
            }
            catch
            {
                // NO BUILDINGS ARE BUILDING
                return List;
            }
        }

        public bool WhatBuildingUnderConstruct()
        {
            try
            {
                chrome.FindElement(By.ClassName("underConstruction"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int FindLevelUnderConstruct()
        {
            try
            {
                var UnderConstruct = chrome.FindElement(By.ClassName("underConstruction"));
                var LevelStr = UnderConstruct.FindElement(By.ClassName("labelLayer")).Text;

                return Convert.ToInt16(new String(LevelStr.Where(Char.IsDigit).ToArray()));
            }
            catch
            {
                MessageBox.Show("ERROR RETURNING LEVEL OF CURRENTLY UPGRADING BUILDING");
                return 0;
            }
        }

        public void close()
        {
            chrome.Close();
            chrome.Quit();
            chrome.Dispose();
        }

        public TravianTaskAdded DoBuildUpgrade(TravianTaskBuild task)
        {
            chrome.Navigate().GoToUrl(task.villageUrl);
            chrome.Navigate().GoToUrl(task.href);
            var content = chrome.FindElement(By.Id("content"));
            var levelStr = content.FindElement(By.ClassName("level")).Text;
            task.level = Convert.ToInt16(new String(levelStr.Where(Char.IsDigit).ToArray()));
            if (task.level >= task.DesiredLvl)
            {
                return new TravianTaskAdded("Done","",0, task.IsResource);
            }
            var upgradeBtn = chrome.FindElement(By.ClassName("upgradeButtonsContainer")).FindElement(By.ClassName("section1")).FindElement(By.TagName("button"));
            if (upgradeBtn.GetAttribute("class").Contains("gold"))
            {
                try
                {
                    content.FindElement(By.ClassName("errorMessage"));
                    return new TravianTaskAdded("Need Resources", "",0, task.IsResource);
                }
                catch
                {
                    return new TravianTaskAdded("Busy", "",0, task.IsResource);
                }
            }
            if (upgradeBtn.GetAttribute("class").Contains("green"))
            {
                var Time = content.FindElement(By.ClassName("clocks")).Text;

                var WhichLevelLongStr = upgradeBtn.FindElement(By.ClassName("button-content")).Text;
                var ButtonLevel = Convert.ToInt16(new String(WhichLevelLongStr.Where(Char.IsDigit).ToArray()));

                if (ButtonLevel > task.DesiredLvl)
                {
                    return new TravianTaskAdded("Done", "", 0, task.IsResource);
                }

                upgradeBtn.Click();
                return new TravianTaskAdded("Doing", Time, ButtonLevel, task.IsResource);
            }
            return new TravianTaskAdded("ERROR", "", 0, task.IsResource);
        }

        public List<TravianVillage> RefreshVillageList()
        {
            List<TravianVillage> Villages = new List<TravianVillage>();
            var sidebar = chrome.FindElement(By.Id("sidebarBoxVillagelist"));
            var inner = sidebar.FindElement(By.ClassName("sidebarBoxInnerBox"));
            var ul = inner.FindElement(By.TagName("ul"));
            var villages = ul.FindElements(By.TagName("li"));
            foreach(IWebElement village in villages)
            {
                var link = village.FindElement(By.TagName("a")).GetAttribute("href");

                var rawX = village.FindElement(By.ClassName("coordinateX")).Text;
                var rawY = village.FindElement(By.ClassName("coordinateY")).Text;


                int x = Convert.ToInt16(new String(rawX.Where(Char.IsDigit).ToArray()));
                int y = Convert.ToInt16(new String(rawY.Where(Char.IsDigit).ToArray()));

                if (rawX.Contains("-"))
                    x = x * (-1);
                if (rawY.Contains("-"))
                    y = y * (-1);

                Villages.Add(new TravianVillage(village.FindElement(By.ClassName("name")).Text, link,new Coordinates(x,y)));
            }
            return Villages;
        }

        public List<Oasis> OasisFinder(int x, int y)
        {
            return new List<Oasis>();
        }

        public TravianVillage resources(TravianVillage village)
        {
            var r1 = new List<string>();
            r1.Add(chrome.FindElement(By.XPath("//*[@id=\"l1\"]")).Text);
            r1.Add(chrome.FindElement(By.XPath("//*[@id=\"l2\"]")).Text);
            r1.Add(chrome.FindElement(By.XPath("//*[@id=\"l3\"]")).Text);
            r1.Add(chrome.FindElement(By.XPath("//*[@id=\"l4\"]")).Text);

            village.Resources = new List<double>();
            foreach(string res in r1)
            {
                village.Resources.Add(Convert.ToInt64(new String(res.Where(Char.IsDigit).ToArray())));
            }
            r1 = new List<string>();
            r1.Add(chrome.FindElement(By.Id("stockBarWarehouse")).Text);
            r1.Add(chrome.FindElement(By.Id("stockBarGranary")).Text);

            village.Storage = new List<long>();
            foreach (string stor in r1)
            {
                village.Storage.Add(Convert.ToInt64(new String(stor.Where(Char.IsDigit).ToArray())));
            }
            try
            {
                chrome.FindElement(By.Id("production")).FindElements(By.ClassName("num"));
            }
            catch
            {
                chrome.Navigate().GoToUrl(urld1);
                chrome.FindElement(By.Id("production")).FindElements(By.ClassName("num"));
            }

            village.Production = new List<double>();
            foreach (IWebElement element in chrome.FindElement(By.Id("production")).FindElements(By.ClassName("num")))
            {
                village.Production.Add(Convert.ToInt64(new String((element.Text).Where(Char.IsDigit).ToArray())));
            }
            return village;
        }
    }
}
