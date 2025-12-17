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
        By tableOfDevicesBy = By.Id("TableOfDevices");
        By errorShownBy = By.Id("ErrorShown");
        By buttonRentDevice = By.Id("rentDeviceButton");

        public SelectDevicesForRental_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) { }

        public void SearchDevices(string model, string priceForRent)
        {
            // Esperar a que el dropdown de modelo esté visible
            WaitForBeingVisible(inputModel);

            // Seleccionar el modelo del dropdown si no es null o vacío
            if (!string.IsNullOrEmpty(model))
            {
                var modelElement = _driver.FindElement(inputModel);
                var selectElement = new SelectElement(modelElement);

                // Seleccionar por el valor (value) del option
                selectElement.SelectByValue(model);

                // Espera breve para que se aplique la selección
                Thread.Sleep(300);
            }

            // Rellenar el campo de precio si no es null o vacío
            if (!string.IsNullOrEmpty(priceForRent))
            {
                WaitForBeingClickable(inputRentPrice);
                var priceElement = _driver.FindElement(inputRentPrice);
                priceElement.Clear();
                Thread.Sleep(100);
                priceElement.SendKeys(priceForRent);
            }

            // Click en el botón de búsqueda
            Thread.Sleep(300);
            WaitForBeingClickable(buttonSearchDevices);
            _driver.FindElement(buttonSearchDevices).Click();

            // Esperar a que se actualicen los resultados
            Thread.Sleep(1500);
        }

        public bool CheckListOfDevices(List<string[]> expectedDevices)
        {
            return CheckBodyTable(expectedDevices, tableOfDevicesBy);
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"Mensaje actual: {actualErrorShown.Text}");
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

        public void RentDevices()
        {
            WaitForBeingClickable(buttonRentDevice);
            _driver.FindElement(buttonRentDevice).Click();
        }

    }
}
