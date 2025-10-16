
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
   

    public class Modelo
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El nombre del producto no puede ser superior a 15 caracteres")]
        public string NameModel { get; set; }

        public IList<Device> Devices { get; set; } //Relacion con Device

        public Modelo() { } //constructor vacio
        public Modelo(int id, string nameModel, IList<Device> devices) { //constructor con parametros
            this.Id = id;
            this.NameModel = nameModel;
            this.Devices = (IList<Device>)devices;

        }

    }
}
