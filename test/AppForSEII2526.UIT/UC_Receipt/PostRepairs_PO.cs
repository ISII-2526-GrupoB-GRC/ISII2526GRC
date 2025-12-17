using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Receipt
{
    public class PostRepairs_PO : PageObject
    {
        private By inputName = By.Id("Name");
        private By inputSurname = By.Id("Surname");
        private By inputAddress = By.Id("Address");
        private By inputPaymentMethod = By.Id("PaymentMethod");
        private By totalCost = By.Id("TotalCost");
        private By errorContainer = By.XPath("//div[@class='row alert alert-danger'][@role='alert']");

        private IWebElement nameElement() => _driver.FindElement(inputName);
        private IWebElement surnameElement() => _driver.FindElement(inputSurname);
        private IWebElement addressElement() => _driver.FindElement(inputAddress);
        private IWebElement paymentMethodElement() => _driver.FindElement(inputPaymentMethod);


        public PostRepairs_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) { }

        public void FillInReceiptInfo(string name, string surname, string deliveryAddress, string paymentMethod) 
        {
            WaitForBeingClickable(inputName);
            _driver.FindElement(inputName).Clear();
            nameElement().SendKeys(name);
            surnameElement().SendKeys(surname);
            addressElement().SendKeys(deliveryAddress);

            SelectElement selectPaymentMethod = new SelectElement(paymentMethodElement());
            selectPaymentMethod.SelectByText(paymentMethod);
        }

        public void FillInModelInfo(string model, string name) {
            WaitForBeingClickable(By.Id("model_"+name));
            _driver.FindElement(By.Id("model_" + name)).Clear();
            _driver.FindElement(By.Id("model_" + name)).SendKeys(model);
        }

        public void PressSubmitReceipt() { 
            _driver.FindElement(By.Id("Submit")).Click();
        }
        
        public void PressModifyReceipt()
        {
            _driver.FindElement(By.Id("ModifyReceipt")).Click();
        }

        public bool CheckListOfReceiptItems(List<string[]> expectedReceiptItems, string model, string name) {
            var result = true;
            result = result && CheckBodyTable(expectedReceiptItems, By.Id("TableOfReceiptItems"));
            result = result && _driver.FindElement(By.Id("model_" + name)).GetAttribute("value") == model;

            return result;
        }

        public bool CheckValidationError(string expectedError) {
            Thread.Sleep(500);
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckTotalPrice(string expectedPrice) {
            WaitForBeingVisible(totalCost);
            string actualCostText = _driver.FindElement(totalCost).Text;
            return actualCostText.Contains(expectedPrice);
        }
    }
}
