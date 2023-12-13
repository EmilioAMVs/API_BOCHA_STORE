using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_BOCHA_STORE.Models
{
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }

        public int idProovedor { get; set; }
        public int idMarca { get; set; }

        [Required]
        public string nombreProducto { get; set; }

        [Required]
        public string descripcionProducto{ get; set; }

        [Required]
        public double precio { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public DateTime fechaCreacion { get; set; }
    }

}
