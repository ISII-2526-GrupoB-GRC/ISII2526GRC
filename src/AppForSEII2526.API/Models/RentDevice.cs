using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

	//Constructores

	public RentDevice() { }

	public RentDevice(int deviceID, double price, int quantity, int rentId)
	{
		DeviceID = deviceID;
		Price = price;
		Quantity = quantity;
		RentId = rentId;
	}

}
