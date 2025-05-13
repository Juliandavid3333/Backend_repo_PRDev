using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaDev.Models
{
    [Table("historial")]
    public class GifSearch
    {
        [Key]
        [Column("id_registro")]
        public int Id { get; set; }
        [Column("fechabusqueda")]
        public DateTime FechaBusqueda { get; set; } = DateTime.UtcNow;

        [Column("catfact")]
        public string FraseCompleta { get; set; } = null!;
        [Column("querytext")]
        public string QueryFact { get; set; } = null!;
        [Column("gifurl")]
        public string GifUrl { get; set; } = null!;
    }
}
