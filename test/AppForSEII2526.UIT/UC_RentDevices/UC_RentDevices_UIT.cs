using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_RentDevices
{
    public class UC_RentDevices_UIT : UC_UIT
    {
        private SelectDevicesForRental_PO selectDevicesForRental_PO;

        private const string name = "Guillermo";
        private const string username = "grosillo";
        private const string surname = "Rosillo Serrano";
        private const string deliveryAddress = "Calle Delivery 1";
        private const string paymentMethod1 = "Credit Card";
        private const string paymentMethod2 = "PayPal";
        private const string paymentMethod3 = "Cash";
        private const string modelo = "Pixel 8a";

        private const int deviceId1 = 1;
        private const string deviceName1 = "iPhone 14 Pro Max";
        private const string deviceBrand1 = "Apple";
        private const string deviceModel1 = "iPhone 14 Pro";
        private const string deviceColor1 = "Negro";
        private const double devicePriceForRent1 = 50.0;

        private const int deviceId2 = 2;
        private const string deviceName2 = "Galaxy S23 Ultra";
        private const string deviceBrand2 = "Samsung";
        private const string deviceModel2 = "Galaxy S23";
        private const string deviceColor2 = "Blanco";
        private const double devicePriceForRent2 = 45.0;

        public UC_RentDevices_UIT(ITestOutputHelper output) : base(output)
        {
            selectDevicesForRental_PO = new SelectDevicesForRental_PO(_driver, _output);
        }

        // Precondición: Usuario logueado
        private void Precondition_perform_login() {
            Perform_login("grosillo@example.com", "Password123!"); 
        }

        private void InitialStepsForRentingDevices()
        {
            //  Precondition_perform_login();
            selectDevicesForRental_PO.WaitForBeingVisible(By.Id("CreateRental"));

            // Click en el menu...
            _driver.FindElement(By.Id("CreateRental")).Click();
        }

        [Theory]
        [InlineData(deviceName1, deviceBrand1, deviceModel1, 2023, deviceColor1, devicePriceForRent1, "iPhone 14 Pro", null)] // Busca por modelo 
        [InlineData(deviceModel2, deviceBrand2, deviceModel2, 2023, deviceColor2, devicePriceForRent2, "", 45.0)]             // Busca por precio
        // [InlineData(deviceName1, deviceBrand1, deviceModel1, deviceColor1, devicePriceForRent1, "iPhone 14 Pro", 50.0)]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC2_AF1_UC2_5_6_filtering(string devicename, string devicebrand, string devicemodel, int año, string devicecolor, double devicepricerent, string filterModel, double filterPriceRent)
        {
            // Arrange
            InitialStepsForRentingDevices();
            var expectedDevices = new List<string[]> { new string[] { devicename, devicebrand, devicemodel, año.ToString(), devicecolor, devicepricerent.ToString() }, };

            //Act
            selectDevicesForRental_PO.SearchDevices(filterModel, filterPriceRent);

            //Assert
            Assert.True(selectDevicesForRental_PO.CheckListOfDevices(expectedDevices));
        }

        public void UC2_AF3_UC2_8_NoDevicesInRentingCart()
        {
            // Arrange
            InitialStepsForRentingDevices();

            // Act
            selectDevicesForRental_PO.AddDeviceToRentingCart(deviceModel1);
            selectDevicesForRental_PO.RemoveDeviceFromRentingCart(deviceModel1);

            // Assert
            Assert.True(selectDevicesForRental_PO.RentingNotAvailable());
        }

        


    }
}