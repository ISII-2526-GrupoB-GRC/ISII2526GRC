using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_RentDevices
{
    public class PostRental_PO : PageObject
    {
        private By Name = By.Id("Name");
        private By Surname = By.Id("Surname");
        private By DeliveryAddress = By.Id("DeliveryAddress");
        private By PaymentMethod = By.Id("PaymentMethod");
        private By TotalPrice = By.Id("TotalPrice");

        private IWebElement _name() => _driver.FindElement(Name);
        private IWebElement _surname() => _driver.FindElement(Surname);
        private IWebElement _deliveryAddress() => _driver.FindElement(DeliveryAddress);
        private IWebElement _paymentMethod() => _driver.FindElement(PaymentMethod);

        public PostRental_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void FillDeviceQuantity(double quantity, string model)
        {
            WaitForBeingClickable(By.Id("quantity_" + model));
            _driver.FindElement(By.Id("quantity_" + model)).Clear();
            _driver.FindElement(By.Id("quantity_" + model)).SendKeys(quantity.ToString());
        }

        private void FillRentalInfo(string name, string surname, string deliveryAddres, string paymentMethod)
        {
            WaitForBeingVisible(Name);
            WaitForBeingVisible(Surname);
            WaitForBeingVisible(DeliveryAddress);
            _name().SendKeys(name);
            _surname().SendKeys(surname);
            _deliveryAddress().SendKeys(deliveryAddres);

            //Elemento de selección para el método de pago
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //Seleccionar opción del DropDown menú
            selectElement.SelectByText(paymentMethod);
        }

        public void PressAllquilarMisDispositivos()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }

        public void PressModifyDevices()
        {
            _driver.FindElement(By.Id("ModifyDevices")).Click();
        }

        public bool CheckListOfDevices(List<String[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("TableOfRentalItems"));
        }

        public bool CheckTotalPrice(string expectedTotalPrice)
        {
            return _driver.FindElement(TotalPrice).GetAttribute("value").Equals(expectedTotalPrice);
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

    }
}
