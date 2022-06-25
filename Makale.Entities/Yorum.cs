using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Makale.Entities
{
    [Table("Yorumlar")]
    public class Yorum:EntityBase
    {
        [Required,StringLength(300)]
        public string YorumText { get; set; }

        public virtual Not Makale { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
