
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
   

    public class Model
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre del producto no puede ser superior a 50 caracteres")]
        public string NameModel { get; set; }

        public IList<Device> Devices { get; set; } //Relacion con Device

        public Model() { } //constructor vacio
        public Model(int id, string nameModel, IList<Device> devices) { //constructor con parametros
            this.Id = id;
            this.NameModel = nameModel;
            this.Devices = (IList<Device>)devices;

        }
        //Para pruebas de GetDevices
        public Model(string nameModel)
        {
            this.NameModel = nameModel;
        }
    }
}
