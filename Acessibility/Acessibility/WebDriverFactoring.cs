using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace Acessibility
{
    public class WebDriverFactoring
    {
        
        public static IWebDriver ObterWebDriver(Browser browser, string caminho_driver = null)
        {
            String caminho_chrome = @"C:\Selenium_C\Chrome\";
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Firefox:
                    webDriver = new FirefoxDriver();
                    break;

                case Browser.Chrome:
                    webDriver = new ChromeDriver(caminho_chrome);
                    webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
                    webDriver.Manage().Window.Maximize();                    

                    break;

                case Browser.IE:
                    webDriver = new InternetExplorerDriver(caminho_driver);
                    break;
            }
            return webDriver;
        }
        
    }
}
