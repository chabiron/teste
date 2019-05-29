using Acessibility.Telas;
using NvdaTestingDriver;
using NvdaTestingDriver.MSTest;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Acessibility.FeatureSteps
{
    [Binding]
    public sealed class StepDefinition1
    {
        private readonly ScenarioContext context;

        public StepDefinition1(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        IWebDriver webDriver;
        internal static NvdaDriver NvdaDriver;
        TelaItau telaItau;


        [Given(@"Que eu inicio o Browser em acessibilidade com o link ""(.*)""")]
        public void DadoQueEuInicioOBrowserEmAcessibilidadeComOLink(string url)
        {
            telaItau = new TelaItau(url);
           
        }

        [Given(@"ativo o Nvda")]
        public async Task Ativo_Nvda()
        {
            await telaItau.Ativar_Nvda();
        }

        [Given(@"inicio o focu em ""(.*)""")]
        public void DadoInicioOFocuEm(string campo)
        {
            telaItau.FocarElemento(campo);
        }

        [When(@"valido os elementos da tela")]
        public async Task EntaoValidoOsElementosDaTela()
        {
            await telaItau.ValidoElementos();
        }

        [Then(@"Fecho o navegador")]
        public void EntaoFechoONavegador()
        {
            telaItau.FecharNavegador();
        }

    

    }
}
