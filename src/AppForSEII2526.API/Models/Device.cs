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
        [StringLength(50, ErrorMessage = "Máximo número de caracteres alcanzado (50)", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio minimo es 0")]
        public double priceForPurchace { get; set; }  //Puede ser 0 ya que podemos querer solo alquilar

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio minimo es 0")]
        public double priceForRent { get; set; }      //Puede ser 0 ya que podemos querer solo vender

        public IList<PurchaseItem> PurchaseItems { get; set; } // Relación con PurchaseItem

        public QualityType Quality { get; set; }

        public enum QualityType
        {
            Low,
            Medium,
            High
        }

        [Required]
        public int quantityForPurchase { get; set; }  //Puede ser 0 ya que podemos querer solo alquilar

        [Required]
        public int quantityForRent { get; set; }      //Puede ser 0 ya que podemos querer solo comprar

        // [Required]
        // public List<ReviewItem> ReviewItems { get; set; }


        [Required]
        public int Year { get; set; }

        
        public Model Model { get; set; } // Relacion con Modelo
        public IList<RentDevice> RentedDevices { get; set; } // Relación con RentDevice


        // Constructores

        public Device() { }

        public Device(string brand, string color, int id, string name, double pricePurchase, double priceRent, IList<PurchaseItem> purchaseItems, QualityType quality, int quantityPurchase, int quantityRent, int year) // IList<ReviewItem> reviewItems
        {
            this.Brand = brand;
            this.Color = color;
            this.Id = id;
            this.Name = name;
            this.priceForPurchace = pricePurchase;
            this.priceForRent = priceRent;
            this.PurchaseItems = purchaseItems;
            this.Quality = quality;
            this.quantityForPurchase = quantityPurchase;
            this.quantityForRent = quantityRent;
            // this.ReviewItems = reviewItems;
            this.Year = year;

        }

        public override bool Equals(object? obj)
        {
            return obj is Device device &&
                   Brand == device.Brand &&
                   Color == device.Color &&
                   Id == device.Id &&
                   Name == device.Name &&
                   priceForPurchace == device.priceForPurchace &&
                   priceForRent == device.priceForRent &&
                   EqualityComparer<IList<PurchaseItem>>.Default.Equals(PurchaseItems, device.PurchaseItems) &&
                   Quality == device.Quality &&
                   quantityForPurchase == device.quantityForPurchase &&
                   quantityForRent == device.quantityForRent &&
                   Year == device.Year &&
                   EqualityComparer<Model>.Default.Equals(Model, device.Model) &&
                   EqualityComparer<IList<RentDevice>>.Default.Equals(RentedDevices, device.RentedDevices);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Brand);
            hash.Add(Color);
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(priceForPurchace);
            hash.Add(priceForRent);
            hash.Add(PurchaseItems);
            hash.Add(Quality);
            hash.Add(quantityForPurchase);
            hash.Add(quantityForRent);
            hash.Add(Year);
            hash.Add(Model);
            hash.Add(RentedDevices);
            return hash.ToHashCode();
        }
    }
}
