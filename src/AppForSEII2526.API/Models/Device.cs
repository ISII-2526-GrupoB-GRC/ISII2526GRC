using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AppForSEII2526.API.Models
{
	public class Device
	{

        [Required]
        [StringLength(50, ErrorMessage = "Máximo número de caracteres alcanzado (50)", MinimumLength = 1)]
        public string Brand { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Máximo número de caracteres alcanzado (30)", MinimumLength = 1)]
        public string Color { get; set; }


        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Máximo número de caracteres alcanzado (100)", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public double priceForPurchace { get; set; }

        [Required]
        public double priceForRent { get; set; }

        public List<PurchaseItem> PurchaseItems { get; set; } // Relación con PurchaseItem

        public QualityType Quality { get; set; }

        public enum QualityType
        {
            Low,
            Medium,
            High
        }

        [Required]
        public int quanityForPurchase { get; set; }

        [Required]
        public int quantityForRent { get; set; }

        // [Required]
        // public List<ReviewItem> ReviewItems { get; set; }


        [Required]
        public int Year { get; set; }

        
        public Modelo Model { get; set; } // Relacion con Modelo
        public IList<RentDevice> RentedDevices { get; set; } // Relación con RentDevice


        // Constructores

        public Device() { }

        public Device(string brand, string color, int id, string name, double pricePurchase, double priceRent, List<PurchaseItem> purchaseItems, QualityType quality, int quantityPurchase, int quantityRent, int year) // IList<ReviewItem> reviewItems
        {
            this.Brand = brand;
            this.Color = color;
            this.Id = id;
            this.Name = name;
            this.priceForPurchace = pricePurchase;
            this.priceForRent = priceRent;
            this.PurchaseItems = purchaseItems;
            this.Quality = quality;
            this.quanityForPurchase = quantityPurchase;
            this.quantityForRent = quantityRent;
            // this.ReviewItems = reviewItems;
            this.Year = year;

        }
	}
}
