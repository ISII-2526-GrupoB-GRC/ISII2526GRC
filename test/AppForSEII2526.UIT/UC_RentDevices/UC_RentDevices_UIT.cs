using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_RentDevices
{
    public class UC_RentDevices_UIT : UC_UIT
    {
        private SelectDevicesForRental_PO selectDevicesForRental_PO;
        public UC_RentDevices_UIT(ITestOutputHelper output) : base(output) 
        {
            Perform_login("grosillo@example.com", "Password123!");
        }

        //Precondición: Usuario logueado
        // private void Precondition_perform_login() { Perform_login("grosillo@example.com", "Password123!"); }

        private void InitialStepsForRentingDevices()
        {
            //Precondition_perform_login();
            selectDevicesForRental_PO.WaitForBeingVisible(By.Id("CreateRental"));
            // Click en el menu...
            _driver.FindElement(By.Id("CreateRental")).Click();
        }

    }
}
