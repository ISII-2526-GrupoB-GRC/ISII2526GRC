using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
	public class RentDevice
	{
		[Required]
		[ForeignKey("Device")]
		public int DeviceId { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		[ForeignKey("Rental")]
		public int RentId { get; set; }

		public IList<Device> Devices { get; set; } // Relación con Device
		public IList<Rental> Rentals { get; set; } // Relación con Rental

		// Constructores

		public RentDevice() { }

		public RentDevice(int deviceID, double price, int quantity, int rentId)
		{
			DeviceId = deviceID;
			Price = price;
			Quantity = quantity;
			RentId = rentId;
		}
	}
}
