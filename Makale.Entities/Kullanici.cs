using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Makale.Entities
{
    [Table("Kullanicilar")]
    public class Kullanici:EntityBase
    {
        [StringLength(25)]
        public string Adi { get; set; }

        [StringLength(25)]
        public string Soyadi { get; set; }

        [Required,StringLength(25)]
        public string KullaniciAdi { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(25)]
        public string Sifre { get; set; }

        [StringLength(30),ScaffoldColumn(false)]
        public string ProfilResmi { get; set; }

        public bool Admin { get; set; }
        public bool Aktif { get; set; }

        [Required,ScaffoldColumn(false)]
        public Guid AktifGuid { get; set; }

        public virtual List<Not> Makaleler { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }
    }
}
