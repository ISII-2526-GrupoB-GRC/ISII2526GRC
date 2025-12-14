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

        public void searchDevice(string Brand,string colour)
        {
            WaitForBeingClickable(inputBrandFilter);
            _driver.FindElement(inputBrandFilter).SendKeys(Brand);
            if (colour == "") colour = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputColourFilter));
            selectElement.SelectByText(colour);
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
    }
}
