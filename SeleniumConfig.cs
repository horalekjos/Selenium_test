using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;


namespace UHK_Selenium_Adam_Kucera
{
    public class SeleniumConfig
    {


        String hubAdress = "http://localhost:4444";
        ChromeOptions options = new ChromeOptions();

        public SeleniumConfig()
        {

        }

        public void PrepareDriver(string name = "Selenium Test")
        {
            options.AddArguments("--headless");
            options.AddArguments("--start-maximized");
            remoteDriver = new RemoteWebDriver(new Uri(hubAdress), options);
        }


        public RemoteWebDriver remoteDriver { get; set; } = default!;

    }
}
