using System.ComponentModel.DataAnnotations;

namespace API_BOCHA_STORE.Models
{
    public class Marca
    {

        [Key]
        public int idMarca { get; set; }

        [Required]
        public string nombreMarca { get; set; }


    }
}
