
using static AppForSEII2526.API.Models.PaymentMethodTypes;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseDetailDTO
    {
        public int id { get; set; } // Id de la compra
        public string name { get; set; }
        public string surname { get; set; }
        public string deliveryAddress { get; set; }
        public DateTime purchaseDate { get; set; }
        public double totalPrice { get; set; }
        public int totalQuantity { get; set; }
        public IList<PurchaseItemDTO> purchaseItems { get; set; }

        public PurchaseDetailDTO(int id, string name, string surname, string deliveryAddress, DateTime purchaseDate, double totalPrice, int totalQuantity, IList<PurchaseItemDTO> purchaseItems)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.deliveryAddress = deliveryAddress;
            this.purchaseDate = purchaseDate;
            this.totalPrice = totalPrice; //esto creo que hay que cambiarlo
            this.totalQuantity = totalQuantity; //esto creo que hay que cambiarlo
            this.purchaseItems = purchaseItems;
        }



        public override bool Equals(object? obj)
        {
            if (obj is PurchaseDetailDTO other)
            {
                return this.name == other.name &&
                       this.surname == other.surname &&
                       this.deliveryAddress == other.deliveryAddress &&
                       this.purchaseDate == other.purchaseDate &&
                       this.totalPrice == other.totalPrice &&
                       this.totalQuantity == other.totalQuantity &&
                       this.purchaseItems.SequenceEqual(other.purchaseItems);


            }
            else return false;
        }
    }
}
