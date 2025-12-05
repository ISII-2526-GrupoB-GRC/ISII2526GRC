using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class RentalStateContainer
    {
        // Cuando un contenedor de estado de alquiler es creado, se crea un alquiler nuevo.
        public RentalForCreateDTO Rental { get; private set; }
            = new RentalForCreateDTO() { RentalItems = new List<RentalItemDTO>() };

        // Cálculo del precio total del alquiler.
        public double TotalPrice
        {
            get
            {
                int numberOfDays = (Rental.RentalDateTo - Rental.RentalDateFrom).Days;
                return Rental.RentalItems.Sum(item => item.PriceForRent * numberOfDays * item.Quantity); // Devolvemos la suma (de cada item) su precio de alquiler * número de días * cantidad a alquila
            }
        }

        public event Action? OnChange; // Evento que se dispara cuando hay un cambio en el estado del alquiler.
        private void NotifyStateChanged() => OnChange?.Invoke(); // Método para notificar cambios en el estado.


        // Método para añadir un dispositivo al alquiler.
        public void AddDeviceToRental(DeviceForRentalDTO device) // Un device es un RentalItem y viceversa.
        {
            // Primero comprobamos si el device no está en la lista de RentalItems, para añadirlo si no está.
            if (!Rental.RentalItems.Any(d => d.Brand == device.Brand && d.NameModel == device.Model)) // Cada dispositivo definido por Marca y Modelo
            {
                Rental.RentalItems.Add(new RentalItemDTO()
                {
                    Brand = device.Brand,
                    NameModel = device.Model,
                    PriceForRent = device.PriceForRent,
                    Quantity = 1
                });
            }
        }

        // Metódo para eliminar un dispositivo del alquiler.
        public void RemoveDeviceFromRental(RentalItemDTO device)
        {
            Rental.RentalItems.Remove(device);
        }

        // Método para eliminar todos los disporitivos de la lista de alquiler.
        public void ClearRental()
        {
            Rental.RentalItems.Clear();
        }

        // Hemos finalizado el proceso de añadir dispositivos al alquiler, entonces, creamos el alquiler.
        public void RentalProcessed()
        {
            // Terminado el proceso de alquiler, reseteamos el estado del contenedor, es decir, creamos un nuevo alquiler vacío.
            Rental = new RentalForCreateDTO() { RentalItems = new List<RentalItemDTO>() };
        }
    }
}
