using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AppForSEII2526.API.Models
{
    public class Purchase
    {
        [Required]
        public string CustomerUserName { get; set; }

        [Required]
        public string CustomerUserSurname { get; set; }

        [Required]
        public String DeliveryAddress { get; set; }

        [Key]
        public int Id { get; set; }

        
        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; } //modificar

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public int TotalQuanty { get; set; }

        public List<PurchaseItem> PurchaseItems { get; set; } //Relacion con PurchaseItem



        public Purchase() { } //constructor vacio


        public Purchase(string customerUserName, string customerUserSurname, String deliveryAddress, int id, PaymentMethodTypes paymentMethod, DateTime purchaseDate, double totalPrice, int totalQuanty, IList<PurchaseItem> purchasesItems) //constructor con parametros
        {
            
            this.CustomerUserName = customerUserName;
            this.CustomerUserSurname = customerUserSurname;
            this.DeliveryAddress = deliveryAddress;
            this.Id = id;
            this.PaymentMethod = paymentMethod;
            this.PurchaseDate = purchaseDate;
            this.TotalPrice = totalPrice;
            this.TotalQuanty = totalQuanty;
            this.PurchaseItems = (List<PurchaseItem>)purchasesItems;
        }


        public enum PaymentMethodTypes
        {
            Cash,
            CreditCard,
            PayPal
        }
    }
}
