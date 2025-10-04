namespace AppForSEII2526.API.Models
{
    public class Receipt
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        //[StringLength(20, ErrorMessage = "Name of Customer can be neither longer than 20 characters nor shorter than 1", MinimumLength = 1)]
        public string CustomerNameSurname { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public IList<ReceiptItem> ReceiptItems { get; set; }
        public Receipt() {
            ReceiptItems = new List<ReceiptItem>();
        }
        public Receipt(int id, string customerNameSurname, string deliveryAddress, PaymentMethodTypes paymentMethod, DateTime receiptDate, double totalPrice)
        {
            this.Id = id;
            this.CustomerNameSurname = customerNameSurname;
            this.DeliveryAddress = deliveryAddress;
            this.PaymentMethod = paymentMethod;
            this.ReceiptDate = receiptDate;
            this.TotalPrice = totalPrice;
        }

        public enum PaymentMethodTypes
        {
            TarjetaCrédito,
            Efectivo,
            PayPal
        }

        public override bool Equals(object obj)
        {
            if (obj is Receipt receipt)
            {
                return Id == receipt.Id && CustomerNameSurname == receipt.CustomerNameSurname && DeliveryAddress == receipt.DeliveryAddress && PaymentMethod == receipt.PaymentMethod && ReceiptDate == receipt.ReceiptDate && TotalPrice == receipt.TotalPrice;
            }
            return false;
        }
    }
}
