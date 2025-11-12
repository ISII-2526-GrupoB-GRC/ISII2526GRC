
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class RentalDetailDTO

    {
        // Este DTO sirve para mostrar la información detallada de un alquiler
        //Apartado 7
        public string Name { get; set; }                // Obtiene de ApplicationUser
        public string Surname { get; set; }             // Obtiene de ApplicationUser
        public string DeliveryAddress { get; set; }
        public DateTime RentalDate { get; set; }        //Obtiene de Rental
        public double TotalPrice { get; set; }          //Obtiene de Rental
        public DateTime RentalDateFrom { get; set; }    //Obtiene de Rental
        public DateTime RentalDateTo { get; set; }      //Obtiene de Rental
        public IList<RentalItemDTO> RentedDevices { get; set; } //Obtiene de RentDevice

        public RentalDetailDTO(string name, string surname, string deliveryAddress, DateTime rentalDate, double totalPrice, DateTime rentalDateFrom, DateTime rentalDateTo, IList<RentalItemDTO> rentedDevices)
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
        public override bool Equals(object? obj)
        {
            return obj is RentalDetailDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   RentalDate == dTO.RentalDate &&
                   TotalPrice == dTO.TotalPrice &&
                   RentalDateFrom == dTO.RentalDateFrom &&
                   RentalDateTo == dTO.RentalDateTo &&
                   RentedDevices.SequenceEqual(dTO.RentedDevices); // Línea cambiada para comparar las listas (Mirar)
        }
    }
}
