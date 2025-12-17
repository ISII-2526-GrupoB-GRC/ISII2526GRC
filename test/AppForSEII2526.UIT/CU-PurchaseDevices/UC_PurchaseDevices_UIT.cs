using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium.DevTools.V141.Storage;

namespace AppForSEII2526.UIT.CU_PurchaseDevices
{
    public class UC_PurchaseDevices_UIT : UC_UIT
    {
        private SelectDevicesForPurchase_PO selectDevicesForPurchase_PO;
        private CreatePurchase_PO createPurchase_PO;
        private const int id1 = 1;
        private const string deviceName1 = "iPhone 14 Pro Max";
        private const string deviceBrand1 = "Apple";
        private const string deviceModel1 = "iPhone 14 Pro";
        private const string deviceColour1 = "Negro";
        private const string devicePrice1 = "1199,99";
        private const string description1 = "muy chulo";


        private const int id2 = 1;
        private const string deviceName2 = "Galaxy S23 Ultra";
        private const string deviceBrand2 = "Samsung";
        private const string deviceModel2 = "Samsung Galaxy S23";
        private const string deviceColour2 = "Blanco";
        private const string devicePrice2 = "1099,99";
        private const string description2 = "muy chulo";

        private const string name = "José María";
        private const string surnames = "Romero Tendero";
        private const string userEmail = "jmromero@example.com";
        private const string deliveryAddress = "Calle Falsa 123, Springfield";
        private const string quantity1 = "1";
        private const string quantity2 = "1";
        private const string paymentMethod1 = "Cash";
        private const string paymentMethod2 = "CreditCard";
        private const string paymentMethod3 = "Paypal";



        public UC_PurchaseDevices_UIT(ITestOutputHelper output) : base(output)
        {
            selectDevicesForPurchase_PO = new SelectDevicesForPurchase_PO(_driver, _output);
            createPurchase_PO = new CreatePurchase_PO(_driver, _output);
        }
        private void Precondition_perform_login()
        {
            Perform_login("jmromero@example.com", "Password123!");
        }

        private void InitialStepsForPurchasingDevices()
        {
            Initial_step_opening_the_web_page();
            //Precondition_perform_login(); //esta linea tiene que permanecer comentada hasta que se arregle el bug del login
            //we wait for the option of the menu to be visible
            selectDevicesForPurchase_PO.WaitForBeingVisible(By.Id("CreatePurchase"));
            //we click on the menu
            _driver.FindElement(By.Id("CreatePurchase")).Click();

        }

        /*[Theory]
        [InlineData(deviceName1, deviceBrand1, deviceModel1, deviceColour1, devicePrice1, paymentMethod1, quantity)]
        [InlineData(deviceName1, deviceBrand1, deviceModel1, deviceColour1, devicePrice1, paymentMethod2, quantity)]
        [InlineData(deviceName1, deviceBrand1, deviceModel1, deviceColour1, devicePrice1, paymentMethod3, quantity)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC_AF1_BasicFlow(string deviceModel, string name, string surnames, string deliveryAddres, string quantity,string description)
        {
            InitialStepsForPurchasingDevices();
            var expectedpurchaseDevices = new List<string[]> { deviceName1,deviceBrand1, deviceModel1, deviceColour1, devicePrice1 };
            selectDevicesForPurchase_PO.AddMovieToPurchasingCart(deviceModel1);
            
        }*/



        [Theory]
        [InlineData(deviceName1,deviceBrand1,deviceModel1, deviceColour1, devicePrice1,"Apple","")]
        [InlineData(deviceName1, deviceBrand1, deviceModel1, deviceColour1, devicePrice1, "", "Negro")]
        [Trait("LevelTesting","Funcional Testing")]
        public void UC_AF1_filtering(string deviceName,string deviceBrand, string deviceModel, string deviceColour,string devicePrice,
            string filterBrand, string filterColour)
        {
            InitialStepsForPurchasingDevices();
            var expectedDevices = new List<string[]> { new string[] { deviceName, deviceBrand, deviceModel, deviceColour, devicePrice }, };

            //Action
            Thread.Sleep(4000);
            selectDevicesForPurchase_PO.searchDevice(filterBrand, filterColour);

            Thread.Sleep(4000); //Esperamos 2 segundos a que cargue la tabla filtrada
            //Assert
            Assert.True(selectDevicesForPurchase_PO.CheckListOfDevices(expectedDevices));

        }

        


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC_AF3()
        {
            InitialStepsForPurchasingDevices();
            selectDevicesForPurchase_PO.AddMovieToPurchasingCart(deviceModel1);
            Thread.Sleep(3000);
            selectDevicesForPurchase_PO.RemoveFromRentingCart(deviceModel1);
            Thread.Sleep(3000);

            Assert.True(selectDevicesForPurchase_PO.PurchasingNotAvailable());

        }
        public void UC_AF0() //dispositivos no disponibles
        {
            InitialStepsForPurchasingDevices();

            selectDevicesForPurchase_PO.searchDevice("", "");

            var expectedMessage = "There are no devices available for being purchase";
            Assert.True(selectDevicesForPurchase_PO.CheckMessageErrorNotAvailableDevices(expectedMessage));

        }

        [Theory]
        [InlineData(deviceModel1,"",surnames,deliveryAddress,paymentMethod1,quantity1, "Errors: (*) Error!. UserName is not registered>")]
        [InlineData(deviceModel1, userEmail, surnames, "", paymentMethod1, quantity1, "You must put a delivery addres")]
        public void UC_AF4_9_10_11(string model, string name, string surnames, string deliveryAddres, string paymentMethod, string quantity, string expectedError)
        {
            InitialStepsForPurchasingDevices();

            selectDevicesForPurchase_PO.AddMovieToPurchasingCart(model);
            selectDevicesForPurchase_PO.PurchaseDevices();

            createPurchase_PO.FillInPurchaseInfo(name, surnames, deliveryAddres, paymentMethod);
            createPurchase_PO.FillInPurchaseQuantity(quantity1, deviceModel1);

            Thread.Sleep(1000);
            createPurchase_PO.PressPurchaseYourDevices();
            Assert.True(createPurchase_PO.CheckValidationError(expectedError),$"Expected error: {expectedError}");
                
        }



        [Fact]
        [Trait("LevelTesting","Funcional Testing")]
        public void CU_AF5_12()
        {
            InitialStepsForPurchasingDevices();

            selectDevicesForPurchase_PO.AddMovieToPurchasingCart(deviceModel1);
            selectDevicesForPurchase_PO.AddMovieToPurchasingCart(deviceModel2);

            selectDevicesForPurchase_PO.PurchaseDevices();

            createPurchase_PO.FillInPurchaseInfo(userEmail,surnames,deliveryAddress,paymentMethod1);
            createPurchase_PO.FillInPurchaseQuantity(quantity1, deviceModel1);
            createPurchase_PO.FillInPurchaseQuantity(quantity2, deviceModel2);

            createPurchase_PO.PressModifyDevices();
            selectDevicesForPurchase_PO.RemoveFromRentingCart(deviceModel2);
            selectDevicesForPurchase_PO.PurchaseDevices();

            var expectedPurchaseDevices = new List<string[]> {
                new string[] { deviceBrand1, deviceModel1, deviceColour1, devicePrice1.Trim(' ', '€')  },
            };
            

            Assert.True(createPurchase_PO.CheckListOfPurchaseDevices(expectedPurchaseDevices,quantity1,deviceModel1));
        }



    }
}
