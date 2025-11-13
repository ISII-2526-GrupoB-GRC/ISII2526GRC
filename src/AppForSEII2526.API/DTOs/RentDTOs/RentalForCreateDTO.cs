namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class RentalForCreateDTO
    {
        //Apartado 5
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        public PaymentMethodTypes PaymentMethod { get; set; }

        public DateTime RentalDateTo { get; set; }
        public DateTime RentalDateFrom { get; set; }
        public IList<RentalItemDTO> RentalItems { get; set; }

        public RentalForCreateDTO() { }

        public RentalForCreateDTO(string name, string surname, string deliveryAddress, PaymentMethodTypes paymentMethod, DateTime rentalDateFrom, DateTime rentalDateTo, IList<RentalItemDTO> rentalItems)
        {

            Name = name;
            Surname = surname;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            RentalDateFrom = rentalDateFrom;
            RentalDateTo = rentalDateTo;
            RentalItems = rentalItems;
        }

        public override bool Equals(object? obj)
        {
            return obj is RentalForCreateDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   RentalDateTo == dTO.RentalDateTo &&
                   RentalDateFrom == dTO.RentalDateFrom &&
                   EqualityComparer<IList<RentalItemDTO>>.Default.Equals(RentalItems, dTO.RentalItems);
        }

    }
}