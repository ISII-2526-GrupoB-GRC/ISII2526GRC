namespace AppForSEII2526.API.DTOs.ReceiptDTOs
{
    public class ReceiptItemDTO
    {
        public string Name { get; set; }
        public string Scale { get; set; }
        public decimal Cost { get; set; }
        public string Model { get; set; }
        public ReceiptItemDTO() { }
        public ReceiptItemDTO(string Name, string Scale, decimal Cost, string Model)
        {
            this.Name = Name;
            this.Scale = Scale;
            this.Cost = Cost;
            this.Model = Model;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReceiptItemDTO dTO &&
                   Name == dTO.Name &&
                   Scale == dTO.Scale &&
                   Cost == dTO.Cost &&
                   Model == dTO.Model;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Scale, Cost, Model);
        }
    }
}