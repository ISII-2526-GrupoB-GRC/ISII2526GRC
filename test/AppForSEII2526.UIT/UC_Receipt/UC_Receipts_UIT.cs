using AppForMovies.UIT.Shared;
using OpenQA.Selenium.DevTools.V141.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Receipt
{
    public class UC_Receipts_UIT : UC_UIT
    {
        private SelectRepairs_PO selectRepairs_PO;
        private const int RepairId1 = 1; 
        private const string repairName1 = "Cambio pantalla";
        private const string repairScale1 = "Lujo";
        private const string cost1 = "150,00 €";
        private const string description1 = "Sustitución completa de pantalla OLED";
        

        private const int RepairId2 = 3; 
        private const string repairName2 = "Reparación puerto carga";
        private const string repairScale2 = "Básica";
        private const string cost2 = "45,00 €";
        private const string description2 = "Reparación del puerto de carga USB-C";

        private const string AddToReceipt = "Add to Receipt";

        private const string name = "Rodrigo";
        private const string username = "rdiaz";
        private const string surname = "Díaz Quintanar";
        private const string deliveryAddress = "Calle Prueba";
        private const string paymentMethod1 = "Credit Card";
        private const string paymentMethod2 = "PayPal";
        private const string paymentMethod3 = "Cash";
        private const string modelo = "Pixel 8a";

        public UC_Receipts_UIT(ITestOutputHelper output) : base(output)
        {
            selectRepairs_PO = new SelectRepairs_PO(_driver, _output);
        }
        /* Como no va aún el login lo dejo comentado 
        private void Precondition_perform_login() {
            Perform_login("rdiaz@example.com", "Password123!");
        }
        */
        private void InitialStepsForReceiptUC() 
        {
            Initial_step_opening_the_web_page();
            //Precondition_perform_login();
            selectRepairs_PO.WaitForBeingVisible(By.Id("CreateReceipt"));
            _driver.FindElement(By.Id("CreateReceipt")).Click();
        }

        [Theory]
        [InlineData(repairName1, username, name, surname, deliveryAddress, paymentMethod1, modelo)]
        [InlineData(repairName1, username, name, surname, deliveryAddress, paymentMethod1, modelo)]
        [InlineData(repairName1, username, name, surname, deliveryAddress, paymentMethod1, modelo)]
        [Trait("Level Testing", "Funcional Testing")]
        public void IC1_CU1_2_3_BasicFlow(string repair, string username, string name , string surname, string deliveryaddress, string paymentmethod, string model) {
            InitialStepsForReceiptUC();

            var expectedReceiptItems = new List<string[]> { 
                new string[]{ repairName1, repairScale1, cost1, modelo},
            };


        }
        [Theory]
        [InlineData(repairName1, repairScale1, cost1, description1,AddToReceipt,"Cambio pan", "")]
        [InlineData(repairName2, repairScale2,cost2, description2, AddToReceipt, "", "Bá")]
        [Trait("Level Testing", "Funcional Testing")]
        public void UC4_AF1_UC5_6_filtering(string repairName, string repairScale, string RepairCost, string RepairDescription, string AddToReceipt,string filterName, string filterScale) //En el gihub de elena tiene otro nombre pero no sé bien que es. Preguntar a Aurora.
        {
            //Arrange
            InitialStepsForReceiptUC();
            var expectedRepairs = new List<string[]>
            {
                new string[] { repairName, repairScale,  RepairCost, RepairDescription, AddToReceipt},
                //new String[] { RepairId2.ToString(), repairName2, repairScale2}
            };
            //Act
            Thread.Sleep(4000); //Esperamos a que cargue la página
            selectRepairs_PO.SearchMovies(filterName, filterScale);
            Thread.Sleep(4000); //Esperamos a que cargue la tabla con los resultados
            //Assert
            Assert.True(selectRepairs_PO.CheckListOfRepairs(expectedRepairs));
        }
     
    }
}
