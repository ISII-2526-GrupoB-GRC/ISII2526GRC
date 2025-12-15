using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_RentDevices
{
    public class SelectDevicesForRental_PO : PageObject
    {
        By inputModel = By.Id("selectModel");
        By inputRentPrice = By.Id("inputRentPrice");
        By buttonSearchDevices = By.Id("searchDevices");
        By tableOfDevicesBy = By.Id("TableOfdevices");
        By errorShownBy = By.Id("ErrorShown");
        By buttonRentDevice = By.Id("rentDeviceButton");

        public SelectDevicesForRental_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) { }

        public void SearchDevices(string model, double priceForRent)
        {
            // Esperar a que el campo de modelo esté visible y luego ingresar el modelo
            WaitForBeingClickable(inputRentPrice);
            _driver.FindElement(inputRentPrice).SendKeys(priceForRent.ToString());
            if (model == "") model = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputModel));
            selectElement.SelectByText(model);

            _driver.FindElement(buttonSearchDevices).Click();

        }

        public bool CheckListOfDevices(List<string[]> expectedDevices)
        {
            return CheckBodyTable(expectedDevices, tableOfDevicesBy);
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"MEnsaje actual: {actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

        public void AddDeviceToRentingCart(string deviceBrand)
        {
            WaitForBeingClickable(By.Id("deviceToRent_" + deviceBrand));

            _driver.FindElement(By.Id("deviceToRent_" + deviceBrand)).Click();
        }
        public void RemoveDeviceFromRentingCart(string deviceBrand)
        {
            WaitForBeingClickable(By.Id("removeDevice_" + deviceBrand));
            _driver.FindElement(By.Id("removeDevice_" + deviceBrand)).Click();
        }

        public bool RentingNotAvailable()
        {
            // El botón no está disponible
            return _driver.FindElement(buttonRentDevice).Displayed == false;
        }


    }
}
