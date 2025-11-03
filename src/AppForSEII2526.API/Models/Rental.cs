
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    using AppForSEII2526.API.DTOs.RentDTOs;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static AppForSEII2526.API.Models.PaymentMethodTypes;

    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de alquiler")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RentalDate { get; set; }

        [DataType(DataType.Date), Display(Name = "Alquiler desde")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RentalDateFrom { get; set; }

        [DataType(DataType.Date), Display(Name = "Alquiler hasta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RentalDateTo { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public IList<RentDevice> RentDevices { get; set; } // Relación con RentDevice (Es lista) -RentDevice = RentalItem en AppForMovies-

        public ApplicationUser ApplicationUser { get; set; } // Relación con ApplicationUser

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DeliveryAddress { get; set; }

        // Constructores

        public Rental() { }

        /*public Rental(PaymentMethodTypes paymentMethod, DateTime rentalDate, DateTime rentalDateFrom, DateTime rentalDateTo, double totalPrice, ApplicationUser applicationUser)
        {
            //Id = id;
            PaymentMethod = paymentMethod;
            RentalDate = rentalDate;
            RentalDateFrom = rentalDateFrom;
            RentalDateTo = rentalDateTo;
            TotalPrice = totalPrice;
            //RentDevice = rentDevice;
            ApplicationUser = applicationUser;
        }*/
        public Rental(string customerName, string customerSurname, string deliveryAddress, DateTime rentalDate, PaymentMethodTypes paymentMethod, DateTime rentalDateFrom, DateTime rentalDateTo, IList<RentDevice> rentalItems, ApplicationUser applicationUser)
        {
            TotalPrice = rentalItems.Sum(ri => ri.Price * (rentalDateTo - rentalDateFrom).Days);
            RentalDateFrom = rentalDateFrom;
            RentalDateTo = rentalDateTo;
            RentalDate = rentalDate;
            DeliveryAddress = deliveryAddress;
            Name = customerName;
            Surname = customerSurname;
            RentDevices = rentalItems;
            PaymentMethod = paymentMethod;
            ApplicationUser = applicationUser;
        }

        public override bool Equals(object? obj)
        {
            return obj is Rental rental &&
                   Id == rental.Id &&
                   PaymentMethod == rental.PaymentMethod &&
                   RentalDate == rental.RentalDate &&
                   RentalDateFrom == rental.RentalDateFrom &&
                   RentalDateTo == rental.RentalDateTo &&
                   TotalPrice == rental.TotalPrice &&
                   EqualityComparer<IList<RentDevice>>.Default.Equals(RentDevices, rental.RentDevices) &&
                   EqualityComparer<ApplicationUser>.Default.Equals(ApplicationUser, rental.ApplicationUser) &&
                   Name == rental.Name &&
                   Surname == rental.Surname &&
                   DeliveryAddress == rental.DeliveryAddress;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(PaymentMethod);
            hash.Add(RentalDate);
            hash.Add(RentalDateFrom);
            hash.Add(RentalDateTo);
            hash.Add(TotalPrice);
            hash.Add(RentDevices);
            hash.Add(ApplicationUser);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            return hash.ToHashCode();
        }
    }
}