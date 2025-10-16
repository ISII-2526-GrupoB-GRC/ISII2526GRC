
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Rental
    {

        public int Id { get; set; }

        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }

        public enum PaymentMethodTypes
        {
            Cash,
            CreditCard,
            PayPal
        }

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

        public RentDevice RentDevice { get; set; } // Relación con RentDevice

        public ApplicationUser ApplicationUser { get; set; } // Relación con ApplicationUser

        // Constructores

        public Rental() { }

        public Rental(int id, DateTime rentalDate, DateTime rentalDateFrom, DateTime rentalDateTo, double totalPrice)
        {

            this.Id = id;
            this.RentalDate = rentalDate; ;
            this.RentalDateFrom = rentalDateFrom;
            this.RentalDateTo = rentalDateTo;
            this.TotalPrice = totalPrice;
        }

    }
}