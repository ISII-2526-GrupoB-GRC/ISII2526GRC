using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static AppForSEII2526.API.Models.PaymentMethodTypes;

namespace AppForSEII2526.API.Models
{
    public class Purchase
    {




        [Required(ErrorMessage = "La direccion de envio es obligatoria")]
        [StringLength(50, ErrorMessage = "La direccion de envio no puede ser mas larga de 50 caracteres")]
        public string DeliveryAddress { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "La forma de pago es obligatoria introducirla")]
        public PaymentMethodTypes PaymentMethod { get; set; } //modificar

        [Required(ErrorMessage = "La fecha de compra es obligatorio introducirlo")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "El precio total de compra es obligatorio introducirlo")]
        [Precision(10, 2)]

        public double TotalPrice { get; set; }

        [Required(ErrorMessage = "La cantidad minima en una compra debe de ser de al meno 1 dispositivo.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima en una compra debe de ser de al meno 1 dispositivo.")]
        public int TotalQuanty { get; set; }

        public IList<PurchaseItem> PurchaseItems { get; set; } //Relacion con PurchaseItem

        public ApplicationUser ApplicationUser { get; set; } // Relación con ApplicationUser

        public Purchase() { } //constructor vacio


        public Purchase(string DeliveryAddress, PaymentMethodTypes paymentMethod, DateTime purchaseDate, double totalPrice, int totalQuanty, IList<PurchaseItem> purchasesItems, ApplicationUser user) //constructor con parametros
        {

            this.DeliveryAddress = DeliveryAddress;
            //this.Id = id;
            this.PaymentMethod = paymentMethod;
            this.PurchaseDate = purchaseDate;
            this.TotalPrice = totalPrice;
            this.TotalQuanty = totalQuanty;
            this.PurchaseItems = (IList<PurchaseItem>)purchasesItems;
            this.ApplicationUser = user;
        }

        public override bool Equals(object? obj)
        {
            return obj is Purchase purchase &&

                   DeliveryAddress == purchase.DeliveryAddress &&
                   Id == purchase.Id &&
                   PaymentMethod == purchase.PaymentMethod &&
                   PurchaseDate == purchase.PurchaseDate &&
                   TotalPrice == purchase.TotalPrice &&
                   TotalQuanty == purchase.TotalQuanty &&
                   EqualityComparer<IList<PurchaseItem>>.Default.Equals(PurchaseItems, purchase.PurchaseItems) &&
                   EqualityComparer<ApplicationUser>.Default.Equals(ApplicationUser, purchase.ApplicationUser);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DeliveryAddress, Id, PaymentMethod, PurchaseDate, TotalPrice, TotalQuanty, PurchaseItems, ApplicationUser);
        }
    }
}
