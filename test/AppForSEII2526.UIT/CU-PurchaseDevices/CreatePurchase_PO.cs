using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_PurchaseDevices
{
    public class CreatePurchase_PO : PageObject
    {

        private By inputName = By.Id("Name");
        private By inputSurnames = By.Id("Surname");
        private By inputDeliveryAddress = By.Id("DeliveryAddress");
        private By inputPaymentMethod = By.Id("PaymentMethod");
        private By totalPrice = By.Id("TotalCost");
        private By errorContainer = By.XPath("//div[@class='row alert alert-danger'][@role='alert']");
        private By purchaseDeviceButton = By.Id("Submit");


        public CreatePurchase_PO(IWebDriver driver,ITestOutputHelper output) : base(driver,output)
        {
        }
        private IWebElement nameElement() => _driver.FindElement(inputName);
        private IWebElement surnamesElement() => _driver.FindElement(inputSurnames);
        private IWebElement deliveryAddressElement() => _driver.FindElement(inputDeliveryAddress);
        private IWebElement paymentMethodElement() => _driver.FindElement(inputPaymentMethod);
        



        public void FillInPurchaseInfo(string name,string surname,string deliveryPurchaseAddres,string paymentMethod)
        {

            WaitForBeingClickable(inputName);
            nameElement().SendKeys(name);
            surnamesElement().SendKeys(surname);
            deliveryAddressElement().SendKeys(deliveryPurchaseAddres);

            SelectElement selectPaymentMethod = new SelectElement(paymentMethodElement());
            selectPaymentMethod.SelectByText(paymentMethod);

        }

        public void FillInPurchaseQuantity(string quantity, string model)
        {
            var quantityInputId = "quantity_" + model;

            WaitForBeingClickable(By.Id(quantityInputId));

            var quantityInput = _driver.FindElement(By.Id(quantityInputId));

            quantityInput.Click();
            quantityInput.SendKeys(Keys.Control + "a");
            quantityInput.SendKeys(Keys.Delete);
            quantityInput.SendKeys(quantity);

            // 🔑 MUY IMPORTANTE para Blazor
            quantityInput.SendKeys(Keys.Tab);
        }
        public void PressPurchaseYourDevices()
        {
            _driver.FindElement(purchaseDeviceButton).Click();
        }

        public void PressModifyDevices() {
            Thread.Sleep(1000);
            _driver.FindElement(By.Id("ModifyDevices")).Click();
        
        }

        public bool CheckListOfPurchaseDevices(List<string[]> expectedPurchaseItems,string quantity,string model)
        {
            return CheckBodyTable(expectedPurchaseItems,By.Id("TableOfPurchaseDevices")) && _driver.FindElement(By.Id("quantity_"+model)).GetAttribute("value")==quantity;
        }

        public bool CheckValidationError(string expectedError)
        {
            Thread.Sleep(500); 
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckTotalPrice(string expectedPrice)
        {
            WaitForBeingClickable(totalPrice);
            string actualPriceText = _driver.FindElement(totalPrice).Text;
            return actualPriceText.Contains(expectedPrice);
        }
    }
}
