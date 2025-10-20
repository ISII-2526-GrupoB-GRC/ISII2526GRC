using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static AppForSEII2526.API.Models.PaymentMethod;

namespace AppForSEII2526.API.Models
{
    public class Purchase
    {

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(10,ErrorMessage = "El nombre de usuario no puede ser mas largo de 1 caracteres")]
        public string CustomerUserName { get; set; }

        [Required(ErrorMessage = "El apellido/s del cliente es obligatorio/s")]
        [StringLength(15, ErrorMessage = "El apellido del usuario no puede ser mas largo de 15 caracteres")]
        public string CustomerUserSurname { get; set; }


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

        [Required(ErrorMessage ="El precio total de compra es obligatorio introducirlo")]
        [Precision(10,2)]

        public double TotalPrice { get; set; }

        [Required(ErrorMessage = "La cantidad minima en una compra debe de ser de al meno 1 dispositivo.")]
        [Range(1,int.MaxValue,ErrorMessage = "La cantidad minima en una compra debe de ser de al meno 1 dispositivo.")]
        public int TotalQuanty { get; set; }

        public IList<PurchaseItem> PurchaseItems { get; set; } //Relacion con PurchaseItem

        public ApplicationUser ApplicationUser { get; set; } // Relación con ApplicationUser

        public Purchase() { } //constructor vacio


        public Purchase(string CustomerUserName, string CustomerUserSurname, string DeliveryAddress, int id, PaymentMethodTypes paymentMethod, DateTime purchaseDate, double totalPrice, int totalQuanty, IList<PurchaseItem> purchasesItems) //constructor con parametros
        {
            this.CustomerUserName = CustomerUserName;
            this.CustomerUserSurname = CustomerUserSurname;
            this.DeliveryAddress = DeliveryAddress;
            this.Id = id;
            this.PaymentMethod = paymentMethod;
            this.PurchaseDate = purchaseDate;
            this.TotalPrice = totalPrice;
            this.TotalQuanty = totalQuanty;
            this.PurchaseItems = (IList<PurchaseItem>)purchasesItems;
        }


        
    }
}
