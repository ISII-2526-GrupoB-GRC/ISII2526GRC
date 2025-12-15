using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Receipt
{
    public class DetailsRepairs_PO : PageObject
    {
        By tableOfDetailsBy = By.Id("RepairsRequested");
        public DetailsRepairs_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) { }
        public bool CheckListOfDetailsRepairs(List<string[]> expectedDetailsRepairs)
        {
            return CheckBodyTable(expectedDetailsRepairs, tableOfDetailsBy);
        }
        public bool CheckReceiptDetail(string nameSurname, string deliveryAddres, DateTime receiptDate, string totalPrice) { //Lo he hecho asi para que sea más fácil a la hora de depurar donde hay un error que cambie el resultado de la prueba
            Thread.Sleep(2000);
            WaitForBeingVisible(By.Id("TotalPrice"));
            bool result = true;
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(nameSurname);
            result = result && _driver.FindElement(By.Id("Address")).Text.Contains(deliveryAddres);
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalPrice);

            var actualReceiptDateText = DateTime.Parse(_driver.FindElement(By.Id("ReceiptDate")).Text);
            result = result && ((actualReceiptDateText - receiptDate) < new TimeSpan (0,1,0)); //Permite un margen de 1 minuto

            return result;
        }

    }
}
