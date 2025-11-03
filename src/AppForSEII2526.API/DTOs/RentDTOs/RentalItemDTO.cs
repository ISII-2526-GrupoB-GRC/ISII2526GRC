namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class RentalItemDTO
    {
        // Este DTO sirve para mostrar la información de cada dispositivo alquilado en un alquiler
        //Apartado 7 (Para cada dispositivo...)
        public string Brand { get; set; } // Nuevo
        public string NameModel { get; set; }         // Obtiene de Model
        public double priceForRent { get; set; }      // Obtiene de Device
        public int quantity { get; set; }             // Obtiene de Device

        public RentalItemDTO() { }

        public RentalItemDTO(string brand, string nameModel, double priceForRent, int quantity)
        {
            Brand = brand;
            NameModel = nameModel;
            this.priceForRent = priceForRent;
            this.quantity = quantity;
        }

        public override bool Equals(object? obj)
        {
            return obj is RentalItemDTO dTO &&
                   NameModel == dTO.NameModel &&
                   priceForRent == dTO.priceForRent &&
                   quantity == dTO.quantity &&
                   Brand == dTO.Brand;
        }
    }
}
