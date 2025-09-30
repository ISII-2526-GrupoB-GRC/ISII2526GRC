using System;

public class Device
{
	public Device()
	{
		public string Brand { get; set; }
		public string Color { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public double PriceForPurchace { get; set; }
		public string PriceForRent { get; set; }
		public List<PurchaseItems> PurchaseItems { get; set; }
		public QualityType Quality { get; set; }
		public int quanityForPurchase { get; set; }
		public int quantityForRent { get; set;}
		public List<ReviewItems> ReviewItems { get; set; }
		public int Year { get; set; }
	}
}
