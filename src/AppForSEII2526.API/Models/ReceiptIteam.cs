namespace AppForSEII2526.API.Models
{
    public class ReceiptIteam
    {
        [Key]
        [Required]
        public string Model { get; set; }
        [Required]
        [ForeignKey("Repair")]
        public int RepairId { get; set; }
        [Required]
        [ForeignKey("Receipt")]
        public int ReceiptId { get; set; }

        public ReceiptIteam() { }
        public ReceiptIteam(string model, int repairId, int receiptId)
        {
            this.Model = model;
            this.RepairId = repairId;
            this.ReceiptId = receiptId;
        }

        public override bool Equals(object obj)
        {
            if (obj is ReceiptIteam receiptIteam)
            {
                return Model == receiptIteam.Model && RepairId == receiptIteam.RepairId && ReceiptId == receiptIteam.ReceiptId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Model, RepairId, ReceiptId);
        }
    }
}
