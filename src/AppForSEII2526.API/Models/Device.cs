using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Operations;

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
        //constructor para purchase
        public Device(string brand, string color, int id, string name, double pricePurchase, int quantityPurchase, int year, Model modelo) // IList<ReviewItem> reviewItems
        {
            this.Brand = brand;
            this.Color = color;
            this.Id = id;
            this.Name = name;
            this.priceForPurchace = pricePurchase;
            //this.priceForRent = priceRent;
            //this.PurchaseItems = purchaseItems;
            // this.Quality = quality;
            this.quantityForPurchase = quantityPurchase;
            //this.quantityForRent = quantityRent;
            // this.ReviewItems = reviewItems;
            this.Year = year;
            this.Model = modelo;

        }

        public Device(string brand, string color, string name, double pricePurchase, int quantityPurchase, int year, Model modelo) // IList<ReviewItem> reviewItems
        {
            this.Brand = brand;
            this.Color = color;

            this.Name = name;
            this.priceForPurchace = pricePurchase;
            //this.priceForRent = priceRent;
            //this.PurchaseItems = purchaseItems;
            // this.Quality = quality;
            this.quantityForPurchase = quantityPurchase;
            //this.quantityForRent = quantityRent;
            // this.ReviewItems = reviewItems;
            this.Year = year;
            this.Model = modelo;

        }
    }
}
