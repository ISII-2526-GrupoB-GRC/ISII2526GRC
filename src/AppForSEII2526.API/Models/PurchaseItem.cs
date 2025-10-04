
using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(DeviceId),nameof(PurchaseID))]
    public class PurchaseItem
    {
        public string? Description { get; set; }

        [Required]
        
        public int DeviceId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        
        public int PurchaseID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Purchase Purchase { get; set; } //Relacion con Purchase
        public Device Device { get; set; } // Relacion con Device



        public PurchaseItem() { } //constructor vacio

        public PurchaseItem(string? description, int quantity, Device device,Purchase purchase) //constructor con parametros
        {
            this.Description = description;
            this.DeviceId = device.Id;
            this.Price = device.PriceForPurchace;
            this.PurchaseID = purchase.Id;
            this.Quantity = quantity;
            this.Device = device;
            this.Purchase = purchase;
        }
    }
}
