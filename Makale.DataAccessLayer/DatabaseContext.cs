using Makale.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace Makale.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Kullanici> Kullanicilar { get; set; }
        public virtual DbSet<Not> Makaleler { get; set; }
        public virtual DbSet<Yorum> Yorumlar { get; set; }
        public virtual DbSet<Kategori> Kategoriler { get; set; }
        public virtual DbSet<Begeni> Begeniler { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer(new DBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Not>().HasMany(n => n.Yorumlar).WithRequired(c => c.Makale).WillCascadeOnDelete(true);

            modelBuilder.Entity<Not>().HasMany(n => n.Begeniler).WithRequired(c => c.Makale).WillCascadeOnDelete(true);


        }

    }


}