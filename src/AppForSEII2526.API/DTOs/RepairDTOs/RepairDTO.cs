namespace AppForSEII2526.API.DTOs.RepairDTOs
{
    public class RepairDTO
    {
        public int Id { get; set; }
        [StringLength(80, ErrorMessage = "La descripción no puede tener más de 80 caracteres")]
        public string? Description { get; set; }
        [Range(0.5, double.MaxValue, ErrorMessage = "El precio minimo de la reparación es 0.50")]
        public decimal Cost { get; set; }
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string? Name { get; set; }
        public string? ScaleName { get; set; }


        // Constructor con parámetros
        public RepairDTO(int id, string? description, decimal cost, string? name, string? scaleName)
        {
            Id = id;
            Description = description;
            Cost = cost;
            Name = name;
            ScaleName = scaleName;
        }

        public override bool Equals(object? obj)
        {
            return obj is RepairDTO dTO &&
                   Id == dTO.Id &&
                   Description == dTO.Description &&
                   Cost == dTO.Cost &&
                   Name == dTO.Name &&
                   ScaleName == dTO.ScaleName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description, Cost, Name, ScaleName);
        }
    }
}