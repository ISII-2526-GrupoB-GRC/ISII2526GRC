using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.CU_PurchaseDevices
{
    public class UC_PurchaseMovies_UIT : UC_UIT
    {
        private SelectDevicesForPurchase_PO selectDevicesForPurchase_PO;
        private const int id = 1;
        private const string deviceName1 = "iPhone 14 Pro Max";
        private const string deviceBrand1 = "Apple";
        private const string deviceModel1 = "iPhone 14 Pro";
        private const string deviceColour1 = "Negro";
        private const string devicePrice1 = "1199,99";


        
        public UC_PurchaseMovies_UIT(ITestOutputHelper output) : base(output)
        {
            selectDevicesForPurchase_PO = new SelectDevicesForPurchase_PO(_driver, _output);
        }
        private void Precondition_perform_login()
        {
            Perform_login("jmromero@example.com", "Password1234");
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

        
        [Theory]
        [InlineData(deviceName1,deviceBrand1,deviceModel1, deviceColour1, devicePrice1,"Apple","")]
        [InlineData(deviceName1, deviceBrand1, deviceModel1, deviceColour1, devicePrice1, "", "Negro")]
        [Trait("LevelTesting","Funcional Testing")]
        public void UC_AF2_filtering(string deviceName,string deviceBrand, string deviceModel, string deviceColour,string devicePrice,
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




    }
}
