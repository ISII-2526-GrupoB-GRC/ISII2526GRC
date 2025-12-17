using static AppForSEII2526.API.Models.PaymentMethodTypes;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseForCreateDTO
    {
        public IList<PurchaseItemDTO> purchaseItems { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce tu nombre")]
        public string name { get; set; }
        //public int id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce tus apellidos")]
        public string surnames { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Dirección de envío")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce tu direccion de envio")]
        public string deliveryAddress { get; set; }

        [Required]
        public PaymentMethodTypes paymentMethod { get; set; }

        public PurchaseForCreateDTO(IList<PurchaseItemDTO> purchaseItems, string name, string surnames, string deliveryAddress, PaymentMethodTypes paymentMethod)
        {
            this.purchaseItems = purchaseItems;
            this.name = name;
            //this.id = id;
            this.surnames = surnames;
            this.deliveryAddress = deliveryAddress;
            this.paymentMethod = paymentMethod;
        }
        public PurchaseForCreateDTO()
        {
            purchaseItems = new List<PurchaseItemDTO>();
        }

        public override bool Equals(object obj)
        {

            if (obj is PurchaseForCreateDTO other)
            {
                return this.purchaseItems.SequenceEqual(other.purchaseItems) &&
                       this.name == other.name &&
                       this.surnames == other.surnames &&
                       this.deliveryAddress == other.deliveryAddress &&
                       this.paymentMethod == other.paymentMethod;
                // this.id == other.id;
            }
            else return false;

        }
    }
}
