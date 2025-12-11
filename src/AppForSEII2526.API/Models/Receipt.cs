using static AppForSEII2526.API.Models.PaymentMethodTypes;

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
        [Range(0.5, double.MaxValue, ErrorMessage = "El precio minimo de la reparación es 0.50")]
        public decimal TotalPrice { get; set; }
        [Required]
        public string deliveryAddres { get; set; }
        public IList<ReceiptItem> ReceiptItems { get; set; }


        public ApplicationUser ApplicationUser { get; set; } // Relación con ApplicationUser

        // Constructores

        public Receipt()
        {
            ReceiptItems = new List<ReceiptItem>();
        }
        public Receipt(PaymentMethodTypes paymentMethod, DateTime receiptDate)
        {

            this.PaymentMethod = paymentMethod;
            this.ReceiptDate = receiptDate;

        }

        public Receipt(PaymentMethodTypes paymentMethod, DateTime receiptDate, IList<ReceiptItem> receiptItems, ApplicationUser applicationUser, string deliveryAddres)
        {
            ReceiptItems = receiptItems;
            ApplicationUser = applicationUser;
            this.TotalPrice = receiptItems.Sum(ri => ri.Repair.Cost);
            this.deliveryAddres = deliveryAddres;
            ReceiptDate = receiptDate;
        }

        public override bool Equals(object obj)
        {
            if (obj is Receipt receipt)
            {
                return PaymentMethod == receipt.PaymentMethod && ReceiptDate == receipt.ReceiptDate && TotalPrice == receipt.TotalPrice && deliveryAddres == receipt.deliveryAddres;
            }
            return false;
        }
    }
}