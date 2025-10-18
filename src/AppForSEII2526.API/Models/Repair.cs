namespace AppForSEII2526.API.Models
{
    public class Repair
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "La descripción no puede tener más de 80 caracteres")]
        public string? Description { get; set; }
        [Required]
        [Range(0.5, float.MaxValue, ErrorMessage = "El precio minimo de la reparación es 0.50")]
        public float Cost { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string Name { get; set; }
        [Required]
        public int ScaleId { get; set; }

        public Repair() { }

        public Repair(int id, string description, float cost, string name, Scale scale)
        {
            this.Id = id;
            this.Description = description;
            this.Cost = cost;
            this.Name = name;
            this.Scale = scale;
            this.ScaleId = scale.Id;
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
