namespace AppForSEII2526.API.DTOs.RentDTOs
{
    public class DeviceForRentalDTO
    {
        // Este DTO sirve para mostrar la información de cada dispositivo disponible para alquiler
        public string Name { get; set; }
        public Model Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double PriceForRent { get; set; }

        public DeviceForRentalDTO(string name, Model model, string brand, int year, string color, double priceForRent)
        {
            Name = name;
            Model = model;
            Brand = brand;
            Year = year;
            Color = color;
            PriceForRent = priceForRent;
        }

    }
}
