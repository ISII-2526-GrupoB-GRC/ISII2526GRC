namespace AppForSEII2526.API.DTOs.DeviceDTOs
{
    public class DeviceForRentalDTO
    {
        // Este DTO sirve para mostrar la información de cada dispositivo disponible para alquiler
        // Apartado 2
        public string Name { get; set; } // Device name
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double PriceForRent { get; set; }

        public DeviceForRentalDTO(string name, string model, string brand, int year, string color, double priceForRent)
        {
            Name = name;
            Model = model;
            Brand = brand;
            Year = year;
            Color = color;
            PriceForRent = priceForRent;
        }

        public override bool Equals(object? obj)
        {
            return obj is DeviceForRentalDTO dTO &&
                   Name == dTO.Name &&
                   Model == dTO.Model &&
                   Brand == dTO.Brand &&
                   Year == dTO.Year &&
                   Color == dTO.Color &&
                   PriceForRent == dTO.PriceForRent;
        }
    }
}