using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace UHK_Selenium_Adam_Kucera
{
    public class Steps
    {
        public void Login(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button/span[text()=' Sign In ']"))).Click();
            driver.FindElement(By.XPath("//span[text()='Company Users']")).Click();
            driver.FindElement(By.Id("userNameInput")).SendKeys("username");
            driver.FindElement(By.Id("passwordInput")).SendKeys("password");
            driver.FindElement(By.Id("submitButton")).Click();
        }

 

        public void Dashboard_SelectViewModule(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("configurator_logo_svg_export"))).Click();
        }

        public void Views_MaterialManagementConfiguration(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/app-platform-root/div/app-platform-material-management/app-platform-material-configuration-dashboard/div/app-platform-basic-widget[2]/mat-card/mat-card-content/a/mat-icon"))).Click();
        }

        public void OpenConcreteView(RemoteWebDriver driver, string viewName)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//app-platform-mmview-card-item")));
            var views = driver.FindElementsByXPath("//app-platform-mmview-card-item");  // ulozi vsechny views do kolekce
            for (int i = 1; i <= views.Count; i++)
            {
                var viewHeader = driver.FindElementByXPath("//app-platform-mmview-card-item[" + i + "]//mat-card-title").Text;  // prvni pohled zacina cislem 1
                if (viewName != viewHeader)
                {
                    continue;
                }
                else
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//app-platform-mmview-card-item[" + i + "]//span[text()=' Open ']"))).Click();
                    break;
                }
            }
        }


        public void View_CreateCalculationFormula(RemoteWebDriver driver, string name)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html[1]/body[1]/app-platform-root[1]/div[1]/app-platform-material-management[1]/app-platform-material-management-view[1]/app-platform-format-line-style-sidenav[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/div[1]/button[1]/span[1]/mat-icon[1]"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div[2]/div/mat-dialog-container/app-platform-calculation-line-dialog/app-platform-shared-dialog/div/mat-card-content/div/form/mat-form-field/div/div[1]/div/input"))).SendKeys(name);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("savebutton"))).Click();
            // ceka se dokud se nezobrazi radek s prepinanim stranek
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[aria-label='Goto Page 1']")));
        }


        public void View_SearchForSpecificCalculationFormula(RemoteWebDriver driver, string name)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            // search
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Grid_searchbar"))).SendKeys(name);
            // resi problem s obcasnym mizenim vlozeneho textu to vyhledavani
            for (int i = 0; i < 10; i++)
            {
                var enteredTextSearch = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("Grid_searchbar"))).GetAttribute("value");

                if (enteredTextSearch != name)
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Grid_searchbar"))).Clear();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Grid_searchbar"))).SendKeys(name);
                    continue;
                }
                else
                {
                    break;
                }
            }
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("td[aria-label='Selenium - Create calculation formula header name']")));

        }


        public void View_DeleteCalculationFormula(RemoteWebDriver driver, string name)    // smaze calculation formula, ktera byla vyhledana podle searchbaru
        {

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html[1]/body[1]/app-platform-root[1]/div[1]/app-platform-material-management[1]/app-platform-material-management-view[1]/app-platform-format-line-style-sidenav[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/div[1]/app-platform-panel[1]/mat-card[1]/mat-card-content[1]/app-platform-single-values[1]/div[1]/ejs-grid[1]/div[4]/div[1]/table[1]/tbody[1]/tr[1]/td[8]/a[4]/span[1]"))).Click();
            // potvrzeni smazani spravne calculation formula podle jmena zadaneho pri vytvoreni
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(By.XPath("/html/body/div[2]/div[2]/div/mat-dialog-container/app-platform-confirmation-dialog/app-platform-shared-dialog/div/mat-card-content/div"), "Do you really want to delete calculation line " + name + " ?"));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div[2]/div/mat-dialog-container/app-platform-confirmation-dialog/app-platform-shared-dialog/div/mat-card-actions/div/button[1]"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Record deleted successfully!')]")));
        }



        public void Dashboard_SearchMaterial(RemoteWebDriver driver, string material, string supplier, string numberOfWeeks, string view)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            MaterialInput_SearchWidget(driver, material);
            SupplierInput_SearchWidget(driver, supplier);
            WeeksSelect_SearchWidget(driver, numberOfWeeks);
            ViewSelect_SearchWidget(driver, view);
            Submit_SearchWidget(driver);
        }


        public void MaterialInput_SearchWidget(RemoteWebDriver driver, string material)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            var materialTextField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("textarea[formControlName=materialNumber]")));
            materialTextField.Clear();
            materialTextField.SendKeys(material);
        }


        public void SupplierInput_SearchWidget(RemoteWebDriver driver, string supplier)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            var supplierTextField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("input[formControlName=supplier]")));
            supplierTextField.Clear();
            supplierTextField.SendKeys(supplier);
        }


        public void WeeksSelect_SearchWidget(RemoteWebDriver driver, string weekNumber) // 1 - 26
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("mat-select[formControlName=weeks]"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option/span[contains(.,'" + weekNumber + "')]"))).Click();
        }


        public void ViewSelect_SearchWidget(RemoteWebDriver driver, string view)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            // resi problem s nekdy dlouho trvajicim nactenim views pro vybrani
            var stop = false;
            for (int i = 0; i < 100; i++)
            {
                if (stop == false)
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("mat-select[formControlName=view]"))).Click();

                    try
                    {
                        var selectMenu = driver.FindElementByXPath("//mat-option").Displayed;
                        if (selectMenu == true)
                        {
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option/span[contains(.,'" + view + "')]"))).Click();
                            stop = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
        }


        public void Submit_SearchWidget(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("submit"))).Click();
        }



        public void EnterMaterialOrder_RequiredFields(RemoteWebDriver driver, string quantity, string identifier, string deliveryDate)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("mat-icon[aria-label='Open new order form']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Qty']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Qty']"))).SendKeys(quantity);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Order id']"))).SendKeys(identifier);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Delivery date']"))).Click();
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Escape).Perform();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Delivery date']"))).SendKeys(deliveryDate);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("submit"))).Click();
            // kontrola zobrazene hlasky po vytvoreni zaznamu
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Material order created.')]")));
        }


        public void SearchForMaterialOrder(RemoteWebDriver driver, string searchParameter) // nalezeni zaznamu podle cisla
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type=search]"))).SendKeys(searchParameter);
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Enter).Perform();
            var idOfFoundOrder = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//td[contains(text(),'" + searchParameter + "')]"))).Text;
            Assert.Equal(searchParameter, idOfFoundOrder);
        }

        public void DeleteMaterialOrder(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='delete']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Are you sure, you want to delete this order?')]"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Delete')]"))).Click();
            // delete order message check
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Order was successfully deleted.')]")));
        }



        public void Dashboard_SelectVirtualGroupModule(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("Path_799"))).Click();

        }


        public void CreateVirtualGroup(RemoteWebDriver driver, string nameOfGroup)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            // klikne na tlacitko pro pridani
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='add']"))).Click();
            // jmeno virtualni skupiny
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='VirtalGroup add']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Insert Group name']"))).SendKeys(nameOfGroup);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div[2]/div/mat-dialog-container/app-platform-add-group/form/mat-dialog-actions/button[2]"))).Click();
            // porovnani
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[contains(text(),'Selenium Virtual Group')]"), nameOfGroup));
        }


        public void DeleteVirtualGroup(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='delete']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Ok']"))).Click();
        }


        public void Dashboard_SelectUserMaterialRightsModule(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//body/app-platform-root[1]/div[1]/app-platform-material-management[1]/app-platform-supply-visibility-dashboard[1]/app-platform-supply-visibility-configure-widget[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/div[1]/div[1]/div[1]/div[1]/gridster[1]/gridster-item[5]/app-platform-configuration-link-widget[1]/mat-card[1]/mat-card-content[1]/a[1]/mat-icon[1]/*[1]"))).Click();
        }


        public void UserMaterialRights_AddRights(RemoteWebDriver driver, string userEmail, string material)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            // klikne na tlacitko pro pridani
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='add']"))).Click();
            // zadani emailu a povolenych zaznamu k videni
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='User Email']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='User Email']"))).SendKeys(userEmail);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//textarea[@placeholder='Material']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//textarea[@placeholder='Material']"))).SendKeys(material);
            // tlacitko ulozit
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("submit"))).Click();
        }


        public void UserMaterialRights_DeleteRights(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='delete']"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//p[contains(text(),'Do you really want to delete this material right')]"), "Do you really want to delete this material rights?"));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Ok']"))).Click();
        }

    }






}
