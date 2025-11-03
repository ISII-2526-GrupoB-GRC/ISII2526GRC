using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
	[PrimaryKey(nameof(DeviceId), nameof(RentalId))]
	public class RentDevice
	{
		[Required]
		public int DeviceId { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		public int RentalId { get; set; } // Cambiado de RentId a RentalId

		public Device Device { get; set; }
		public Rental Rental { get; set; }

		public RentDevice() { }

		//
		public RentDevice(double price, int quantity, Device device, Rental rental)
		{
			Price = price;
			Quantity = quantity;
			Device = device;
			DeviceId = device.Id;
			Rental = rental;
			RentalId = rental.Id; // Cambiado
        }
        //

        public RentDevice(double price, int quantity, int deviceId, Rental rental)
        {
            Price = price;
            Quantity = quantity;
            DeviceId = deviceId;
            Rental = rental;
            RentalId = rental.Id; // Cambiado
        }

        public override bool Equals(object? obj)
		{
			return obj is RentDevice device &&
				   DeviceId == device.DeviceId &&
				   Price == device.Price &&
				   Quantity == device.Quantity &&
				   RentalId == device.RentalId && // Cambiado
				   EqualityComparer<Device>.Default.Equals(Device, device.Device) &&
				   EqualityComparer<Rental>.Default.Equals(Rental, device.Rental);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(DeviceId, Price, Quantity, RentalId, Device, Rental); // Cambiado
		}
	}
}
