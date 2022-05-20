//File to open the Tests class, which contains tests

using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace UHK_Selenium_Adam_Kucera
{

    [CollectionDefinition("Selenium collection 1")] public class SeleniumCollection1 : ICollectionFixture<SeleniumConfig> { }


    [Collection("Selenium collection 1")]

    public class Tests
    {
        SeleniumConfig fix;
        public Tests(SeleniumConfig fix)
        {
            this.fix = fix;
            this.fix.PrepareDriver("Tests");
        }

        String url = "https://app-url.com";



        [Fact(DisplayName = "Valid login")]
        public void ValidLogin()
        {
            try
            {
                var wait = new WebDriverWait(fix.remoteDriver, new TimeSpan(0, 0, 5));
                Steps step = new Steps();

                fix.remoteDriver.Navigate().GoToUrl(url);
                step.Login(fix.remoteDriver);
            }
            finally
            {
                fix.remoteDriver.Quit();
            }
        }



        [Theory(DisplayName = "Calculation formula - Create, Search, Delete")]
        [InlineData("view", "Selenium - Calculation formula")]
        public void CreateCalculationFormula(string view, string nameOfCalculationFormula)
        {
            try
            {
                Steps step = new Steps();
                fix.remoteDriver.Navigate().GoToUrl(url);
                step.Login(fix.remoteDriver);
                step.Dashboard_SelectViewModule(fix.remoteDriver);
                step.OpenConcreteView(fix.remoteDriver, view);
                step.View_CreateCalculationFormula(fix.remoteDriver, nameOfCalculationFormula);
                step.View_SearchForSpecificCalculationFormula(fix.remoteDriver, nameOfCalculationFormula);
                step.View_DeleteCalculationFormula(fix.remoteDriver, nameOfCalculationFormula);
            }
            finally
            {
                fix.remoteDriver.Quit();
            }
        }



        [Theory(DisplayName = "Material order (Required fields)")]
        [InlineData("material", "supplier", "numberOfWeeks", "view", "quantity", "identifier", "deliveryDate")]
        public void MaterialOrder_RequiredFields(string material, string supplier, string numberOfWeeks, string view, string quantity, string identifier, string deliveryDate)
        {
            try
            {
                Steps step = new Steps();
                fix.remoteDriver.Navigate().GoToUrl(url);
                step.Login(fix.remoteDriver);
                step.Dashboard_SearchMaterial(fix.remoteDriver, material, supplier, numberOfWeeks, view);
                step.EnterMaterialOrder_RequiredFields(fix.remoteDriver, quantity, identifier, deliveryDate);
                step.SearchForMaterialOrder(fix.remoteDriver, identifier);
                step.DeleteMaterialOrder(fix.remoteDriver);
            }
            finally
            {
                fix.remoteDriver.Quit();
            }
        }


        [Theory(DisplayName = "Virtual group - Create, Delete")]
        [InlineData("Selenium Virtual Group")]
        public void VirtualGroup(string virtualGroupName)
        {
            try
            {
                Steps step = new Steps();
                fix.remoteDriver.Navigate().GoToUrl(url);
                step.Login(fix.remoteDriver);
                step.Dashboard_SelectVirtualGroupModule(fix.remoteDriver);
                step.CreateVirtualGroup(fix.remoteDriver, virtualGroupName);
                step.DeleteVirtualGroup(fix.remoteDriver);
            }
            finally
            {
                fix.remoteDriver.Quit();
            }

        }

            [Theory(DisplayName = "User material rights - Create, Delete")]
            [InlineData("userEmail", "material")]
            public void UserMaterialsRights(string userEmail, string material)
            {
                try
                {
                    Steps step = new Steps();
                    fix.remoteDriver.Navigate().GoToUrl(url);
                    step.Login(fix.remoteDriver);
                    step.Dashboard_SelectUserMaterialRightsModule(fix.remoteDriver);
                    step.UserMaterialRights_AddRights(fix.remoteDriver, userEmail, material);
                    step.UserMaterialRights_DeleteRights(fix.remoteDriver);
                }
                finally
                {
                    fix.remoteDriver.Quit();
                }



            }

        }
}
