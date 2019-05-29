using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using NvdaTestingDriver;
using NvdaTestingDriver.MSTest;
using NvdaTestingDriver.Selenium.Extensions;


namespace Acessibility.Telas
{
    public class TelaItau
    {

        IWebDriver webDriver;
        internal static NvdaDriver NvdaDriver;

        public TelaItau(string url)
        {
            webDriver = WebDriverFactoring.ObterWebDriver(Browser.Chrome);
            webDriver.Navigate().GoToUrl(url);
            webDriver.FocusOnWindow();
            
        }

        public void FocarElemento(string campo)
        {
            webDriver.Focus(webDriver.FindElement(By.LinkText("serviços")));
            
            
        }

        public async Task ValidoElementos()
        {
            Thread.Sleep(10000);
            string fala = await NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
            Console.WriteLine("Botão Ajuda = " + fala);
            NvdaAssert.TextContains(fala, "botão recolhido ajuda");
        }


        public async Task Ativar_Nvda()
        {
            await ConnectNvdaDriverAsync();
        }

        public static async Task ConnectNvdaDriverAsync()
        {
            try
            {
                // We start the NvdaTestingDriver:
                NvdaDriver = new NvdaDriver(opt =>
                {
                    opt.GeneralSettings.Language = NvdaTestingDriver.Settings.NvdaLanguage.PortugueseBrazil;
                });
                await NvdaDriver.ConnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while starting NVDA driver: {ex.Message}");
                throw;
            }
        }

        public void FecharNavegador()
        {
            webDriver.Close();

        }
    }
}
