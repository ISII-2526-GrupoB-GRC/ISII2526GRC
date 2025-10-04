namespace AppForSEII2526.API.Models
{
    public class Repair
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ScaleId { get; set; }

        public Repair() { }
        
        public Repair(int id, string description, float cost, string name, int scaleId)
        {
            this.Id = id;
            this.Description = description;
            this.Cost = cost;
            this.Name = name;
            this.ScaleId = scaleId;
        }

        //Relaciones entre clases
        public Scale Scale { get; set; }
        public IList<ReceiptItem> ReceiptItems { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is Repair repair)
            {
                return Id == repair.Id &&
                       Description == repair.Description &&
                       Cost == repair.Cost &&
                       Name == repair.Name &&
                       ScaleId == repair.ScaleId;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description, Cost, Name, ScaleId);
        }
    }
}
