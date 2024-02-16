using OpenQA.Selenium;

namespace Reqnroll.Actions.LambdaTest.IntegrationTests.PageObjects
{
    public class CalculatorElementLocators
    {
        //The URL of the calculator to be opened in the browser
        private protected const string CalculatorUrl = "https://reqnrolloss.github.io/Calculator-Demo/Calculator.html";

        //Finding elements by ID
        private protected By FirstNumberFieldLocator => By.Id("first-number");
        private protected By SecondNumberFieldLocator => By.Id("second-number");
        private protected By AddButtonLocator => By.Id("add-button");
        private protected By ResultLabelLocator => By.Id("result");
        private protected By ResetButtonLocator => By.Id("reset-button");
    }
}