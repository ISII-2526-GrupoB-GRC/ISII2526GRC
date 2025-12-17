using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.CU_PurchaseDevices
{
    public class SelectDevicesForPurchase_PO : PageObject
    {

        By inputBrandFilter = By.Id("inputBrand");
        By inputColourFilter = By.Id("inputColour");
        By buttonSearchDevices = By.Id("searchDevices");
        By tableOfDevices = By.Id("TableOfMovies"); //aqui es contradictorio pero hubo un pequeño error al crearlo y a estas alturas del proyecto lo más rentable es tirar pa lante si funciona
        By buttonPurchaseDevices = By.Id("purchaseDeviceButton");
        By buttonAddToCart = By.Id("deviceToPurchase_@device");


        public SelectDevicesForPurchase_PO(IWebDriver driver, ITestOutputHelper output):base(driver,output)
        {

        }

        public void searchDevice(string brand, string colour)
        {
            WaitForBeingClickable(inputBrandFilter);
            _driver.FindElement(inputBrandFilter).SendKeys(brand);


            //En el proyecto de la asignatura se hizo Elena un controlador para los géneros disponibles que se podría aplicar aquí con las escalas disponibles. Preguntar sí es necesario y sí en caso de no tenerlo baja nota
            WaitForBeingClickable(inputColourFilter);
            _driver.FindElement(inputColourFilter).SendKeys(colour);

            _driver.FindElement(buttonSearchDevices).Click();
        }


        public bool CheckListOfDevices(List<string[]> expectedMovies)
        {
            return CheckBodyTable(expectedMovies, tableOfDevices);
        }
       


        public void AddMovieToPurchasingCart(string model)
        {
            WaitForBeingClickable(By.Id("deviceToPurchase_"+model));
            _driver.FindElement(By.Id("deviceToPurchase_" + model)).Click();
        }


        public void RemoveFromRentingCart(string model)
        {
            WaitForBeingClickable(By.Id("removeDevice_" + model));
            _driver.FindElement(By.Id("removeDevice_" + model)).Click();
        }

        public bool PurchasingNotAvailable()
        {

            return _driver.FindElement(buttonPurchaseDevices).Displayed == false;
        }



        public bool CheckMessageErrorNotAvailableDevices(string expectedError)


        {


            return _driver.PageSource.Contains(expectedError);


        }
        public void PurchaseDevices()


        {


            WaitForBeingClickable(buttonPurchaseDevices);


            _driver.FindElement(buttonPurchaseDevices).Click();


        }
    }
}
