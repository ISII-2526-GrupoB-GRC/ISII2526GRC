using AppForMovies.UIT.Shared;
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
        private const int RepairId1 = 1; //Comprobar que este id coincide con el de la base de datos inicial
        private const string repairName1 = "Cambio pantalla";
        private const string repairScale1 = "Lujo";

        private const string repairName2 = "Cambio batería";
        private const string repairScale2 = "Medio";

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
            //Precondition_perform_login();
            selectRepairs_PO.WaitForBeingVisible(By.Id("CreateReceipt"));
            _driver.FindElement(By.Id("CreateReceipt")).Click();
        }
        [Theory]
        [InlineData(repairName1, repairScale1, "Cambio pan", "")]
        [InlineData(repairName2, repairScale2, "", "Medio")]
        [Trait("Level Testing", "Funcional Testing")]
        public void UC4_AF1_UC5_6_filtering(string repairName, string repairScale, string filterName, string filterScale) //En el gihub de elena tiene otro nombre pero no sé bien que es. Preguntar a Aurora.
        {
            //Arrange
            InitialStepsForReceiptUC();
            var expectedRepairs = new List<String[]>
            {
                new String[] { RepairId1.ToString(), repairName1, repairScale1 }
            };
            //Act
            selectRepairs_PO.SearchMovies(filterName, filterScale);

            //Assert
            Assert.True(selectRepairs_PO.CheckListOfRepairs(expectedRepairs));
        }
     
    }
}
