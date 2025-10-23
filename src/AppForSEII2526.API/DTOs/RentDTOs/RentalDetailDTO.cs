namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class RentalDetailDTO

    {
        // Este DTO sirve para mostrar la información detallada de un alquiler
        public string Name { get; set; }
        public string Surname { get; set; }
        // public string DeliveryAddress { get; set; }
        public DateTime RentalDate { get; set; }
        public double TotalPrice { get; set; }
        public DateTime RentalDateFrom { get; set; }
        public DateTime RentalDateTo { get; set; }
        public IList<RentDeviceDTO> RentedDevices { get; set; }

        public RentalDetailDTO(string name, string surname, DateTime rentalDate, double totalPrice, DateTime rentalDateFrom, DateTime rentalDateTo, IList<RentDeviceDTO> rentedDevices)
        {
            Name = name;
            Surname = surname;
            RentalDate = rentalDate;
            TotalPrice = totalPrice;
            RentalDateFrom = rentalDateFrom;
            RentalDateTo = rentalDateTo;
            RentedDevices = rentedDevices;
        }
    }
}
