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
        By tableOfRepairsBy = By.Id("TableofRepairs");
        public SelectRepairs_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) { }
        public void SearchMovies(string name, string scale) {
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
    }
}
