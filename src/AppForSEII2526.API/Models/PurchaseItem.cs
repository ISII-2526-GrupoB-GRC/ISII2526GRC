
using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(DeviceId), nameof(PurchaseID))]
    public class PurchaseItem
    {
        [StringLength(50, ErrorMessage = "La descripcion no puede tener mas de 50 caracteres.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El ID del dispositivo es obligatorio.")]

        public int DeviceId { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "El ID de compra es obligatorio.")]

        public int PurchaseID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima que se compra del producto debe ser 1.")]
        public int Quantity { get; set; }

        public Purchase Purchase { get; set; } //Relacion con Purchase
        public Device Device { get; set; } // Relacion con Device



        public PurchaseItem() { } //constructor vacio

        /*
        public PurchaseItem(string? description, int quantity, Device device,Purchase purchase) //constructor con parametros
        {
            this.Description = description;
            this.DeviceId = device.Id;
            this.Price = device.priceForPurchace;
            this.PurchaseID = purchase.Id;
            this.Quantity = quantity;
            this.Device = device;
            this.Purchase = purchase;
        }*/

        public PurchaseItem(string? description, int quantity, int deviceId, Purchase purchase) //constructor con parametros
        {
            this.Description = description;
            this.DeviceId = deviceId;
            this.Price = 0; //se asignara mas tarde
            this.PurchaseID = purchase.Id;
            this.Quantity = quantity;
            this.Purchase = purchase;
        }


        public PurchaseItem(int quantity, Device device)
        {
            this.Quantity = quantity;
            this.Device = device;





        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseItem item &&
                   Description == item.Description &&
                   DeviceId == item.DeviceId &&
                   Price == item.Price &&
                   PurchaseID == item.PurchaseID &&
                   Quantity == item.Quantity &&
                   EqualityComparer<Purchase>.Default.Equals(Purchase, item.Purchase) &&
                   EqualityComparer<Device>.Default.Equals(Device, item.Device);
        }
    }
}
