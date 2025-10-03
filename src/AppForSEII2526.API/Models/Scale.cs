namespace AppForSEII2526.API.Models
{
    public class Scale
    {
        //Primary key
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Scale() { }

        public Scale(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public override bool Equals(object obj)
        {
            if (obj is Scale scale)
            {
                return Id == scale.Id && Name == scale.Name;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
