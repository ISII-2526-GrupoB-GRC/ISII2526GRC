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
        public UC_Receipts_UIT(ITestOutputHelper output) : base(output)
        {

        }
        /* Como no va aún el login lo dejo comentado 
        private void Precondition_perform_login() {
            Perform_login("rdiaz@example.com", "Password123!");
        }
        */
    }
}
