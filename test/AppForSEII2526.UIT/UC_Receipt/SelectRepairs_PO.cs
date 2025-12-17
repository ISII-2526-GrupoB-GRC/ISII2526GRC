using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Receipt
{
    public class SelectRepairs_PO : PageObject
    {
        By inputName = By.Id("inputName");
        By inputScale = By.Id("inputScale");
        By buttonSearchRepairs = By.Id("searchRepairs");
        By RepairButton = By.Id("RepairButton");
        By tableOfRepairsBy = By.Id("TableofRepairs");
        public SelectRepairs_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) { }
        public void SearchRepairs(string name, string scale) {
            WaitForBeingClickable(inputName);
            _driver.FindElement(inputName).SendKeys(name);
            

            //En el proyecto de la asignatura se hizo Elena un controlador para los géneros disponibles que se podría aplicar aquí con las escalas disponibles. Preguntar sí es necesario y sí en caso de no tenerlo baja nota
            WaitForBeingClickable(inputScale);
            _driver.FindElement(inputScale).SendKeys(scale);

            _driver.FindElement(buttonSearchRepairs).Click();
        }
        public bool CheckListOfRepairs(List<String[]> expectedRepairs) 
        {
            return CheckBodyTable(expectedRepairs, tableOfRepairsBy);
        }
        public void AddRepairToReceipt(string repairName) 
        {
            WaitForBeingClickable(By.Id("repair_"+repairName));
            _driver.FindElement(By.Id("repair_" + repairName)).Click();
        }
        public void RemoveRepairFromReceipt(string repairName) 
        {
            WaitForBeingClickable(By.Id("removeRepair_"+repairName));
            _driver.FindElement(By.Id("removeRepair_" + repairName)).Click();
        }
        public void DoReceipt() 
        {
            WaitForBeingClickable(RepairButton);
            _driver.FindElement(RepairButton).Click();
        }
        public bool ReceiptNotAvaible() { 
            return _driver.FindElement(RepairButton).Displayed == false;
        }
        public bool CheckMessageErrorNotAviableRepairs(string expectedMessage) { 
            return _driver.PageSource.Contains(expectedMessage);
        }
    }
}
