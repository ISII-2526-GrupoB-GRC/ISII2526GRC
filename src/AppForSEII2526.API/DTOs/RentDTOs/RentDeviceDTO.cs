namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class RentDeviceDTO
    {
        // Este DTO sirve para mostrar la información de cada dispositivo alquilado en un alquiler
        public string NameModel { get; set; }         // Obtiene de Model
        public double priceForRent { get; set; } // Obtiene de Device
        public int Quantity { get; set; }        // Obtiene de RentDevice

        public RentDeviceDTO(string model, double priceRent, int quantity)
        {
            NameModel = model;
            priceForRent = priceRent;
            Quantity = quantity;
        }

    }
}
