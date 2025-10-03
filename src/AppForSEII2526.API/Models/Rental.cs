using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
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
            TarjetaCrédito,
            Efectivo,
            PayPal
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RentalDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RentalDateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RentalDateTo { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        // Constructores:

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