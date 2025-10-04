
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Rental
    {
        [Required]
        [StringLength(200, ErrorMessage = "Máximo número de caracteres alcanzado (200)", MinimumLength = 1)]
        public string DeliveryAdress { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Máximo número de caracteres alcanzado (100)", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Máximo número de caracteres alcanzado (100)", MinimumLength = 1)]
        public string Surname { get; set; }

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

        // Constructores

        public Rental() { }

        public Rental(string deliveryAdress, int id, string name, string surname, DateTime rentalDate, DateTime rentalDateFrom, DateTime rentalDateTo, double totalPrice)
        {
            this.DeliveryAdress = deliveryAdress;
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.RentalDate = rentalDate; ;
            this.RentalDateFrom = rentalDateFrom;
            this.RentalDateTo = rentalDateTo;
            this.TotalPrice = totalPrice;
        }

    }
}