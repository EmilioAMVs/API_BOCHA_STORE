using System.ComponentModel.DataAnnotations;

namespace API_BOCHA_STORE.Models
{
    public class Proovedor
    {
        [Key]
        public int idProovedor { get; set; }

        [Required]
        public string nombreProovedor { get; set; }

        [Required]
        public int duracionContrato { get; set; }

        [Required]
        public double precioImportacion { get; set; }
    }
}
