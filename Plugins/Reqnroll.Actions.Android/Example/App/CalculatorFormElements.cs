using OpenQA.Selenium.Appium.Android;
using Reqnroll.Actions.Android.Driver;

namespace Example.App
{
    public class CalculatorFormElements
    {
        private readonly AndroidAppDriver _androidAppDriver;

        public CalculatorFormElements(AndroidAppDriver androidAppDriver)
        {
            _androidAppDriver = androidAppDriver;
        }

        public AndroidElement FirstNumberTextBox =>
            _androidAppDriver.Current.FindElementById("com.companyname.reqnrollcalculator:id/firstNumberTextBox");

        public AndroidElement SecondNumberTextBox =>
            _androidAppDriver.Current.FindElementById("com.companyname.reqnrollcalculator:id/secondNumberTextBox");

        public AndroidElement AddButton =>
            _androidAppDriver.Current.FindElementById("com.companyname.reqnrollcalculator:id/addButton");

        public AndroidElement SubtractButton =>
            _androidAppDriver.Current.FindElementById("com.companyname.reqnrollcalculator:id/subtractButton");

        public AndroidElement MultiplyButton =>
            _androidAppDriver.Current.FindElementById("com.companyname.reqnrollcalculator:id/multiplyButton");

        public AndroidElement DivideButton =>
            _androidAppDriver.Current.FindElementById("com.companyname.reqnrollcalculator:id/divideButton");

        public AndroidElement ResultTextBox =>
            _androidAppDriver.Current.FindElementById("com.companyname.reqnrollcalculator:id/resultTextBox");
    }
}