namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class RentalDetailDTO

    {
        // Este DTO sirve para mostrar la información detallada de un alquiler
        public string Name { get; set; }                // Obtiene de ApplicationUser
        public string Surname { get; set; }             // Obtiene de ApplicationUser
        public string DeliveryAddress { get; set; }
        public DateTime RentalDate { get; set; }        //Obtiene de Rental
        public double TotalPrice { get; set; }          //Obtiene de Rental
        public DateTime RentalDateFrom { get; set; }    //Obtiene de Rental
        public DateTime RentalDateTo { get; set; }      //Obtiene de Rental
        public IList<RentDeviceDTO> RentedDevices { get; set; } //Obtiene de RentDevice

        public RentalDetailDTO(string name, string surname, string deliveryAddress, DateTime rentalDate, double totalPrice, DateTime rentalDateFrom, DateTime rentalDateTo, IList<RentDeviceDTO> rentedDevices)
        {
            Name = name;
            Surname = surname;
            DeliveryAddress = deliveryAddress;
            RentalDate = rentalDate;
            TotalPrice = totalPrice;
            RentalDateFrom = rentalDateFrom;
            RentalDateTo = rentalDateTo;
            RentedDevices = rentedDevices;
        }
    }
}
