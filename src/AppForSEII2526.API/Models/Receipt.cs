namespace AppForSEII2526.API.Models
{
    public class Receipt
    {
        
        [Required]
        public int Id { get; set; }

        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReceiptDate { get; set; }
        [Required]
        [Precision(10, 2)]
        [Range(0.5, double.MaxValue, ErrorMessage = "El precio minimo es 0.50")]
        public double TotalPrice { get; set; }
        public IList<ReceiptItem> ReceiptItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; } // Relación con ApplicationUser

        // Constructores

        public Receipt() {
            ReceiptItems = new List<ReceiptItem>();
        }
        public Receipt(int id, PaymentMethodTypes paymentMethod, DateTime receiptDate, double totalPrice)
        {
            this.Id = id;
            this.PaymentMethod = paymentMethod;
            this.ReceiptDate = receiptDate;
            this.TotalPrice = totalPrice;
        }

        public enum PaymentMethodTypes
        {
            Cash,
            CreditCard,
            PayPal
        }

        /*public override bool Equals(object obj)
        {
            if (obj is Receipt receipt)
            {
                return Id == receipt.Id && CustomerNameSurname == receipt.CustomerNameSurname && DeliveryAddress == receipt.DeliveryAddress && PaymentMethod == receipt.PaymentMethod && ReceiptDate == receipt.ReceiptDate && TotalPrice == receipt.TotalPrice;
            }
            return false;
        }*/
    }
}
