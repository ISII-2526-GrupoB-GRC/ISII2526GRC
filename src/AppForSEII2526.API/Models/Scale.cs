
namespace AppForSEII2526.API.Models
{
    public class Scale
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
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

        //Relaciones entre clases
        public IList<Repair> Repairs { get; set; }

    }
}
