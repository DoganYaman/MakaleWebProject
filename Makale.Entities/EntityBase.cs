using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime KayitTarihi { get; set; }
     
        [Required]
        public DateTime DegistirmeTarihi { get; set; }

        [Required,StringLength(30)]
        public string DegistirenKullanici { get; set; }
    }
}
