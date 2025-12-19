using AppForSEII2526.UIT.CU_PurchaseDevices;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.UC_RentDevices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

{
    
}

namespace AppForSEII2526.UIT.UC_RentDevices
{
    public class UC_RentDevices_UIT : UC_UIT
    {
        private SelectDevicesForRental_PO selectDevicesForRental_PO;
        private DetailsRental_PO detailsRental_PO;
        private PostRental_PO postRental_PO;

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
        private const string devicePriceForRent1 = "50 €";
        private const int quantity1 = 1;

        private const int deviceId2 = 2;
        private const string deviceName2 = "Galaxy S23 Ultra";
        private const string deviceBrand2 = "Samsung";
        private const string deviceModel2 = "Samsung Galaxy S23";
        private const string deviceColor2 = "Blanco";
        private const string devicePriceForRent2 = "45 €";
        private const int quantity2 = 2;

        public UC_RentDevices_UIT(ITestOutputHelper output) : base(output)
        {
            selectDevicesForRental_PO = new SelectDevicesForRental_PO(_driver, _output);
            detailsRental_PO = new DetailsRental_PO(_driver, _output);
            postRental_PO = new PostRental_PO(_driver, _output);

        }

        // Precondición: Usuario logueado
        private void Precondition_perform_login() {
            Perform_login("grosillo@example.com", "Password123!"); 
        }

        private void InitialStepsForRentingDevices()
        {
            Initial_step_opening_the_web_page();
            Precondition_perform_login();

            // Espera de seguridad para que cargue la página principal
            Thread.Sleep(2000);

            var byCreateRental = By.Id("CreateRental");
            selectDevicesForRental_PO.WaitForBeingVisible(byCreateRental);

            // Click en el menu...
            _driver.FindElement(byCreateRental).Click();
            
            // Espera para que cargue la página de selección de dispositivos
            Thread.Sleep(1500);
        }

        [Theory]
        [InlineData(deviceName1, deviceModel1, deviceBrand1, "2023", deviceColor1, devicePriceForRent1, "iPhone 14 Pro", "")]
        [InlineData(deviceName2, deviceModel2, deviceBrand2, "2023", deviceColor2, devicePriceForRent2, "", "45")]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC2_AF1_UC2_5_6_filtering(string devicename, string devicemodel, string devicebrand, string año, string devicecolor, string devicepricerent, string filterModel, string filterPriceRent)
        {
            // Arrange
            InitialStepsForRentingDevices();
            
            // Espera para que cargue la página completamente
            Thread.Sleep(1000);
            
            // El orden debe coincidir con las columnas HTML: Name | Brand | Model | Brand | Año | Color | Price
            var expectedDevices = new List<string[]> { 
                new string[] { 
                    devicename,      // "iPhone 14 Pro Max" o "Galaxy S23 Ultra"
                    devicemodel,     // "iPhone 14 Pro" o "Galaxy S23"
                    devicebrand,     // "Apple" o "Samsung"
                    año,             // "2023"
                    devicecolor,     // "Negro" o "Blanco"
                    devicepricerent  // "50" o "45"
                } 
            };

            //Act
            selectDevicesForRental_PO.SearchDevices(filterModel, filterPriceRent);
            
            // Espera para que se filtren los resultados
            Thread.Sleep(1500);

            //Assert
            Assert.True(selectDevicesForRental_PO.CheckListOfDevices(expectedDevices));
        }

        // Prueba Modificar dispositivos seleccionados:
        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC2_AF2_UC2_7_ModifySelectedDevices()
        {
            // Arrange
            InitialStepsForRentingDevices();
            var expectedDevices = new List<string[]> {
            new string[] {
                deviceBrand1,
                deviceModel1,
                devicePriceForRent1
            },
            };

            // Act
            selectDevicesForRental_PO.AddDeviceToRentingCart(deviceBrand1);
            selectDevicesForRental_PO.AddDeviceToRentingCart(deviceBrand2);
            selectDevicesForRental_PO.RentDevices();

            postRental_PO.FillDeviceQuantity(quantity1, deviceBrand1);
            postRental_PO.FillDeviceQuantity(quantity2, deviceBrand2);
            postRental_PO.FillRentalInfo(surname, deliveryAddress, paymentMethod3);

            postRental_PO.PressModifyDevices();
            selectDevicesForRental_PO.RemoveDeviceFromRentingCart(deviceBrand2);
            selectDevicesForRental_PO.RentDevices();

            // Assert
            Assert.True(postRental_PO.CheckListOfDevices(expectedDevices, quantity1.ToString(), deviceBrand1));
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC2_AF3_UC2_8_NoDevicesInRentingCart()
        {
            // Arrange
            InitialStepsForRentingDevices();

            // Act
            selectDevicesForRental_PO.AddDeviceToRentingCart(deviceBrand1);
            selectDevicesForRental_PO.RemoveDeviceFromRentingCart(deviceBrand1);

            // Assert
            Assert.True(selectDevicesForRental_PO.RentingNotAvailable());
        }


        [Theory]
        [InlineData(deviceBrand1, name, surname, deliveryAddress, paymentMethod2, quantity1)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_1_BasicFlow(string devicebrand, string name, string surname, string deliveryAddress, string paymentMethods, int quantity)
        {
            //Arrange
            InitialStepsForRentingDevices();
            var expectedRentedDevices = new List<string[]> {
            new string[] {
                deviceBrand1,
                deviceModel1,
                devicePriceForRent1.ToString(),
                quantity1.ToString(),
            }
            };

            //Act
            selectDevicesForRental_PO.AddDeviceToRentingCart(devicebrand);
            
            // Espera después de añadir al carrito
            Thread.Sleep(1000);
            
            selectDevicesForRental_PO.RentDevices();
            
            // Espera CRÍTICA para que cargue la página PostRental
            Thread.Sleep(1000);
            
            // Verificar que los elementos están visibles antes de interactuar
            postRental_PO.WaitForBeingVisible(By.Id("Surname"));
            postRental_PO.WaitForBeingVisible(By.Id("DeliveryAddress"));
            postRental_PO.WaitForBeingVisible(By.Id("PaymentMethod"));

            postRental_PO.FillRentalInfo(surname, deliveryAddress, paymentMethods);
            postRental_PO.FillDeviceQuantity(quantity, devicebrand);
            
            // Espera antes de presionar el botón
            Thread.Sleep(1000);
            
            postRental_PO.PressAllquilarMisDispositivos();
            
            // Espera para que aparezca el modal
            Thread.Sleep(1000);
            
            postRental_PO.PressOkModalDialog();
            
            // ESPERA CRÍTICA: Debe cargar la página DetailRental completamente
            Thread.Sleep(1000);
            
            // Esperar a que el elemento RentalDate esté visible antes de verificar
            detailsRental_PO.WaitForBeingVisible(By.Id("RentalDate"));
            detailsRental_PO.WaitForBeingVisible(By.Id("Surname"));
            detailsRental_PO.WaitForBeingVisible(By.Id("DeliveryAddress"));
            detailsRental_PO.WaitForBeingVisible(By.Id("TotalPrice"));

            //Assert
            // Compruebo el detalle:
            Assert.True(detailsRental_PO.CheckRentalDetail(surname, deliveryAddress, DateTime.Today, 50.0, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));
        }

        // Modificación Examen Sprint 3: ***
        [Theory]
        [InlineData(surname, deliveryAddress, paymentMethod2, quantity1, deviceModel2)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void Examen_Sprint3(string surname, string deliveryAddress, string paymentMethods, int quantity, string filtroModelo)
        {
            //Arrange
            InitialStepsForRentingDevices();

            //Act

            selectDevicesForRental_PO.AddDeviceToRentingCart(deviceBrand1); // 1) Añade elemento
            selectDevicesForRental_PO.SearchDevices(filtroModelo, "");      // 2) Filtra por modelo
            selectDevicesForRental_PO.AddDeviceToRentingCart(deviceBrand2); // 3) Añade elemento
            selectDevicesForRental_PO.RemoveDeviceFromRentingCart(deviceBrand1); // 4) Elimina 1er elemento

            selectDevicesForRental_PO.RentDevices();                             // 5) Resto del proceso...

            postRental_PO.FillRentalInfo(surname, deliveryAddress, paymentMethods);
            postRental_PO.FillDeviceQuantity(quantity, deviceBrand2);
            postRental_PO.PressAllquilarMisDispositivos();
            postRental_PO.PressOkModalDialog();

            //Assert
            // Compruebo el detalle:
            Assert.True(detailsRental_PO.CheckRentalDetail(surname, deliveryAddress, DateTime.Today, 45.0, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));
        }

    }
}