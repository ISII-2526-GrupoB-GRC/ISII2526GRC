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
        public UC_RentDevices_UIT(ITestOutputHelper output) : base(output) { }

        //Precondición: Usuario logueado
        // private void Precondition_perform_login() { Perform_login("grosillo@example.com", "Password123!"); }

    }
}
