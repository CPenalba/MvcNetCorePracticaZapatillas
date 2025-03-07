using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcNetCorePracticaZapatillas.Models
{
    [Table("ZAPASPRACTICA")]
    public class Zapatilla
    {
        [Key]
        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("PRECIO")]
        public int Precio { get; set; }
    }
}
