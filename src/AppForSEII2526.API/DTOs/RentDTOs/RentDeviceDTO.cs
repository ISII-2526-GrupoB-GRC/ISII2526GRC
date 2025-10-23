namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class RentDeviceDTO
    {
        // Este DTO sirve para mostrar la información de cada dispositivo alquilado en un alquiler
        public Model Model { get; set; }
        public double priceForRent { get; set; }
        public int Quantity { get; set; }

        public RentDeviceDTO(Model model, double priceRent, int quantity)
        {
            Model = model;
            priceForRent = priceRent;
            Quantity = quantity;
        }

    }
}
