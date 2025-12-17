using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_RentDevices
{
    public class DetailsRental_PO : PageObject
    {
        public DetailsRental_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckRentalDetail(string surname, string delivery, DateTime rentalDate, double totalPrice, DateTime rentalDateFrom, DateTime rentalDateTo)
        {
            WaitForBeingVisible(By.Id("TotalPrice"));
            bool result = true;
            //result = result && _driver.FindElement(By.Id("Name")).Text.Contains(name);
            result = result && _driver.FindElement(By.Id("Surname")).Text.Contains(surname);
            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(delivery);
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalPrice.ToString());

            var actualDate = DateTime.Parse(_driver.FindElement(By.Id("RentalDate")).Text);
            result = result && actualDate.Date == rentalDate.Date;

            result = result && _driver.FindElement(By.Id("RentalPeriod")).Text.Contains($"{rentalDateFrom.ToShortDateString()} - {rentalDateTo.ToShortDateString()}");

            return result;
        }

        public bool CheckListOfDevices(List<string[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("RentedDevices"));
        }

    }
}
