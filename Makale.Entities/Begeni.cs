using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.Entities
{
    [Table("Begeniler")]
    public class Begeni
    {
        [Key]
        public int Id { get; set; }
        public virtual Not Makale { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
