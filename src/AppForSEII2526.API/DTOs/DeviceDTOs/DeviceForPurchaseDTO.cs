
namespace AppForSEII2526.API.DTOs.DeviceDTOs
{
    public class DeviceForPurchaseDTO
    {
        public string name { get; set; }
        public string Brand { get; set; }
        public string nameModel { get; set; }
        public string colour { get; set; }
        public double price { get; set; }

        public DeviceForPurchaseDTO(string name, string marca, string nameModel, string colour, double price)
        {
            this.name = name;
            Brand = marca;
            this.nameModel = nameModel;
            this.colour = colour;
            this.price = price;
        }


        public override bool Equals(object? obj)
        {
            if (obj is DeviceForPurchaseDTO other)
            {
                return name == other.name &&
                       Brand == other.Brand &&
                       nameModel == other.nameModel &&
                       colour == other.colour &&
                       price == other.price;


            }
            else return false;
        }


    }
}
