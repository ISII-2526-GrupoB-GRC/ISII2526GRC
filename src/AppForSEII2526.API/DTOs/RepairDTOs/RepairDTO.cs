namespace AppForSEII2526.API.DTOs.RepairDTOs
{
    public class RepairDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float Cost { get; set; }
        public string Name { get; set; }
        public int ScaleId { get; set; }

        // Constructor con parámetros
        public RepairDTO(int id, string? description, float cost, string name, int scaleId)
        {
            Id = id;
            Description = description;
            Cost = cost;
            Name = name;
            ScaleId = scaleId;
        }

    }
}
