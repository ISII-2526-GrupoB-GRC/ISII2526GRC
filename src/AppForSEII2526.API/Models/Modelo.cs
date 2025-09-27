
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




    }
}
