
namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseItemDTO
    {
        public string brand { get; set; }
        public string nameModel { get; set; }
        public string colour { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string description { get; set; }

        public PurchaseItemDTO(string brand, string namemodel, string colour, double price, int quantity, string description)
        {
            this.brand = brand;
            this.nameModel = namemodel;
            this.colour = colour;
            this.price = price;
            this.quantity = quantity;
            this.description = description;
        }

        public override bool Equals(object? obj)
        {
            if (obj is PurchaseItemDTO other)
            {
                return this.brand == other.brand &&
                       this.nameModel == other.nameModel &&
                       this.colour == other.colour &&
                       this.price == other.price &&
                       this.quantity == other.quantity &&
                       this.description == other.description;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(brand, nameModel, colour, price, quantity, description);
        }
    }
}
