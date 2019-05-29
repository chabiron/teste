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
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace Acessibility
{
    [TestFixture]
    public class Tests
    {
        IWebDriver webDriver;
        internal static NvdaDriver NvdaDriver;
        private ExtentReports extent;
        ExtentReports report;
        ExtentHtmlReporter htmlReporter;
        ExtentTest test;

        [OneTimeSetUp]
        public async Task setUpOnce()
        {
            htmlReporter = new ExtentHtmlReporter(@"C:\Selenium_C\Itau.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.DocumentTitle = "Test Report | Itau";
            htmlReporter.Config.ReportName = "Test Report | Itau";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            webDriver = WebDriverFactoring.ObterWebDriver(Browser.Chrome);
            webDriver.Navigate().GoToUrl("https://www.itau.com.br");
            webDriver.FocusOnWindow();
            await ConnectNvdaDriverAsync();


        }


        [SetUp]
        public void Iniciar()
        {

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public async Task validarBotaoAjuda()
        {
                        
                webDriver.FindElement(By.LinkText("ajuda")).Click();
                webDriver.Focus(webDriver.FindElement(By.LinkText("serviços")));
                Thread.Sleep(10000);
                string fala = await NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
                Console.WriteLine("msg = " + fala);
                NvdaAssert.TextContains(fala, "botãoo recolhido ajuda");           
        }

        [Test]
        public async Task validarBotaoBusca()
        {

            webDriver.Focus(webDriver.FindElement(By.LinkText("ajuda")));
            Thread.Sleep(3000);
            string fala = await NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
            Console.WriteLine("msg = " + fala);
            NvdaAssert.TextContains(fala, "fora de lista botão de menu sub Menu busca");


        }
        [TearDown]
        public void validarRelatorio()
        {
            {
                try
                {
                    var status = TestContext.CurrentContext.Result.Outcome.Status;
                    var stacktrace = "" +TestContext.CurrentContext.Result.StackTrace + "";
                    var errorMessage = TestContext.CurrentContext.Result.Message;
                    Status logstatus;
                    switch (status)
                    {
                        case TestStatus.Failed:
                            logstatus = Status.Fail;
                            //string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                            test.Log(logstatus, "Test ended with " +logstatus + " – " +errorMessage);
                            //test.Log(logstatus, "Snapshot below: " +test.AddScreenCaptureFromPath(screenShotPath));
                            break;
                        case TestStatus.Skipped:
                            logstatus = Status.Skip;
                            test.Log(logstatus, "Test ended with " +logstatus);
                            break;
                        default:
                            logstatus = Status.Pass;
                            test.Log(logstatus, "Test ended with " +logstatus);
                            break;
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
           
        }

        [OneTimeTearDown]
        public void fecharBrowser()
        {
            extent.Flush();
            webDriver.Close();
        }

        private static async Task ConnectNvdaDriverAsync()
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
    }
}
