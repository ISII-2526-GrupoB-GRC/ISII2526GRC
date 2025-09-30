namespace AppForSEII2526.API.Models
{
    public class Repair
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [ForeignKey("Scale")]
        public int ScaleId { get; set; }

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
