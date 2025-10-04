
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
   

    public class Modelo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NameModel { get; set; }

        public List<Device> Devices { get; set; } //Relacion con Device

        public Modelo() { } //constructor vacio
        public Modelo(int id, string nameModel, IList<Device> devices) { //constructor con parametros
            this.Id = id;
            this.NameModel = nameModel;
            this.Devices = (List<Device>)devices;

        }

    }
}
