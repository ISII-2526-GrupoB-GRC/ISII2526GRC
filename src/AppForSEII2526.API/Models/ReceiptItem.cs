namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(RepairId), nameof(ReceiptId))]
    public class ReceiptItem
    {
        [Required]
        [StringLength(15, ErrorMessage = "El nombre del modelo no puede ser superior a 15 caracteres")]
        public string Model { get; set; }
        public Repair Repair { get; set; } //Relacion con Repair
        [Required]
        public int RepairId { get; set; }
        public Receipt Receipt { get; set; } //Relacion con Receipt
        [Required]
        public int ReceiptId { get; set; }

        public ReceiptItem() { }
        public ReceiptItem(string model, Repair? repair, Receipt receipt)
        {
            this.Model = model;
            this.Repair = repair;
            this.RepairId = repair.Id;
            this.Receipt = receipt;
            this.ReceiptId = receipt.Id;
        }


        public override bool Equals(object obj)
        {
            if (obj is ReceiptItem receiptIteam)
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