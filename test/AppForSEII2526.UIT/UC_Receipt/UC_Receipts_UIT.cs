using AppForSEII2526.UIT.Shared;
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
        private PostRepairs_PO postRepairs_PO;
        private DetailsRepairs_PO detailsRepairs_PO;

        private const int RepairId1 = 1;
        private const string repairName1 = "Cambio pantalla";
        private const string repairScale1 = "Lujo";
        private const string cost1 = "150,00 €";
        private const string cost1NoDecimals = "150 €";
        private const string cost1Number = "150";
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
            postRepairs_PO = new PostRepairs_PO(_driver, output);
            detailsRepairs_PO = new DetailsRepairs_PO(_driver, output);
        }
         
        private void Precondition_perform_login() {
            Perform_login("rdiaz@example.com", "Password123!");
        }
        
        private void InitialStepsForReceiptUC()
        {
            Initial_step_opening_the_web_page();
            Precondition_perform_login();
            selectRepairs_PO.WaitForBeingVisible(By.Id("CreateReceipt"));
            _driver.FindElement(By.Id("CreateReceipt")).Click();
        }

        [Theory]
        [InlineData(repairName1, username, name, surname, deliveryAddress, paymentMethod1, modelo)]
        [InlineData(repairName1, username, name, surname, deliveryAddress, paymentMethod2, modelo)]
        [InlineData(repairName1, username, name, surname, deliveryAddress, paymentMethod3, modelo)]
        [Trait("Level Testing", "Funcional Testing")]
        public void UC4_UC1_2_3_BasicFlow(string repair, string username, string name, string surname, string deliveryaddress, string paymentmethod, string model)
        {
            InitialStepsForReceiptUC();

            var expectedReceiptItems = new List<string[]> {
                new string[]{ repairName1, repairScale1, cost1Number, modelo},
            };

            //Act
            selectRepairs_PO.AddRepairToReceipt(repair);
            selectRepairs_PO.DoReceipt();

            postRepairs_PO.FillInReceiptInfo(username, (name + " " + surname), deliveryaddress, paymentmethod);
            postRepairs_PO.FillInModelInfo(model, repair);
            postRepairs_PO.PressSubmitReceipt();
            postRepairs_PO.PressOkModalDialog();
            //Assert
            Assert.True(detailsRepairs_PO.CheckReceiptDetail(name+" "+surname,deliveryaddress, DateTime.Now, cost1NoDecimals), "Receipt details are not correct");
            Assert.True(detailsRepairs_PO.CheckListOfDetailsRepairs(expectedReceiptItems), "Receipt items are not correct");

        }
        [Fact]
        [Trait("Level Testing", "Funcional Testing")]
        public void UC4_AF0_UC4_RepairsNotAvailable()
        {
            //Arrange
            InitialStepsForReceiptUC();
            var expectedMessage = "Error fetching repairs. ";

            //Act
            selectRepairs_PO.SearchRepairs("", "");

            //Assert
            Assert.True(selectRepairs_PO.CheckMessageErrorNotAviableRepairs(expectedMessage));
        }
        [Theory]
        [InlineData(repairName1, repairScale1, cost1, description1, AddToReceipt, "Cambio pan", "")]
        [InlineData(repairName2, repairScale2, cost2, description2, AddToReceipt, "", "Bá")]
        [Trait("Level Testing", "Funcional Testing")]
        public void UC4_AF1_UC5_6_filtering(string repairName, string repairScale, string RepairCost, string RepairDescription, string AddToReceipt, string filterName, string filterScale) //En el gihub de elena tiene otro nombre pero no sé bien que es. Preguntar a Aurora.
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
            selectRepairs_PO.SearchRepairs(filterName, filterScale);
            Thread.Sleep(4000); //Esperamos a que cargue la tabla con los resultados
            //Assert
            Assert.True(selectRepairs_PO.CheckListOfRepairs(expectedRepairs));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF2_UC_7_UpdateShoppingCart()
        {
            //Arrange
            InitialStepsForReceiptUC();
            var expectedCost = "150";

            //Act
            selectRepairs_PO.AddRepairToReceipt(repairName1);
            selectRepairs_PO.AddRepairToReceipt(repairName2);
            selectRepairs_PO.DoReceipt();

            postRepairs_PO.FillInModelInfo(modelo, repairName1);
            postRepairs_PO.FillInModelInfo(modelo, repairName2);
            postRepairs_PO.PressModifyReceipt();

            selectRepairs_PO.RemoveRepairFromReceipt(repairName2);
            selectRepairs_PO.DoReceipt();

            //Assert
            Assert.True(postRepairs_PO.CheckTotalPrice(expectedCost));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF3_UC1_8_ReceiptNotAvailable()
        {
            //Arrange
            InitialStepsForReceiptUC();

            //Act
            selectRepairs_PO.AddRepairToReceipt(repairName1);
            selectRepairs_PO.RemoveRepairFromReceipt(repairName1);

            //Assert
            Assert.True(selectRepairs_PO.ReceiptNotAvaible());
        }
        [Theory]
        [InlineData("", (name + " " + surname), deliveryAddress, paymentMethod2, modelo, "The Username field is required.")]
        [InlineData(username, "", deliveryAddress, paymentMethod2, modelo, "The Usersurname field is required.")]
        [InlineData(username, (name + " " + surname), "", paymentMethod2, modelo, "The Userdeliveryaddress field is required.")]
        [InlineData(username, (name + " " + surname), deliveryAddress, paymentMethod2, "", "The Model field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF4_UC9_10_11_12_ValidationErrors(string name, string surname, string deliveryaddress, string paymentmethod, string model, string expectedError)
        {
            //Arrange
            InitialStepsForReceiptUC();
            //Act
            selectRepairs_PO.AddRepairToReceipt(repairName1);
            selectRepairs_PO.DoReceipt();
            postRepairs_PO.FillInReceiptInfo(name, surname, deliveryaddress, paymentmethod);
            postRepairs_PO.FillInModelInfo(model, repairName1);
            postRepairs_PO.PressSubmitReceipt();
            //Assert
            Assert.True(postRepairs_PO.CheckValidationError(expectedError));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF5_UC13_ModifyReceipt() 
        {
            InitialStepsForReceiptUC();
            var expectedReceiptItems = new List<string[]>
            {
                new string[] { repairName1, repairScale1, cost1Number},
            };

            //Act
            selectRepairs_PO.AddRepairToReceipt(repairName1);
            selectRepairs_PO.AddRepairToReceipt(repairName2);
            selectRepairs_PO.DoReceipt();

            postRepairs_PO.FillInModelInfo(modelo, repairName1);
            postRepairs_PO.FillInModelInfo(modelo, repairName2);
            postRepairs_PO.FillInReceiptInfo(username, (name + " " + surname), deliveryAddress, paymentMethod2);
            
            postRepairs_PO.PressModifyReceipt();
            selectRepairs_PO.RemoveRepairFromReceipt(repairName2);
            selectRepairs_PO.DoReceipt();

            //Assert
            Assert.True(postRepairs_PO.CheckListOfReceiptItems(expectedReceiptItems, modelo, repairName1));

        }
    }
}
