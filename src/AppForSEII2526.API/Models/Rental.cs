using System;

public class Rental
{
	public Rental()
	{
		public string DeliveryAdress {  get; set; }
	    public int Id { get; set; }
		public string Name { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public DateTime RentalDate { get; set; }
		public DateTime RentalDateFrom { get; set; }
		public DateTime RentalDateTo { get; set; }
		public string Surname { get; set; }
		public double TotalPrice { get; set; }
	}
}
