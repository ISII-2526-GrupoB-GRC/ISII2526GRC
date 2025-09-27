
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AppForSEII2526.API.Models
{
    public class PurchaseItem
    {
        public string? Description { get; set; }

        [Required]
        [ForeignKey("Device")]
        public int DeviceId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [ForeignKey("Purchase")]
        public int PurchaseID { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
