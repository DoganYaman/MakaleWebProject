using Makale.DataAccessLayer;
using Makale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.BusinessLayer
{
    public class Test
    {
        Repository<Kullanici> repo_kul = new Repository<Kullanici>();
        Repository<Kategori> repo_kat = new Repository<Kategori>();
        public Test()
        {
            //DatabaseContext db = new DatabaseContext();
            //db.Kullanicilar.ToList();   

            List<Kategori> katlist = repo_kat.List();  

            List<Kullanici> kullist = repo_kul.List();
        }

        public void InsertTest()
        {
            repo_kul.Insert(new Kullanici() 
            { 
              Adi="xx",
              Soyadi="yy",
              KullaniciAdi = "zz",
              Email ="xx@yy",
              Sifre="123",
              Aktif=true,
              Admin=true,
              AktifGuid=Guid.NewGuid(),
              KayitTarihi=DateTime.Now,
              DegistirmeTarihi=DateTime.Now.AddMinutes(5),
              DegistirenKullanici="elif"
            });
        }

        public void UpdateTest()
        {
            Kullanici kul = repo_kul.Find(x => x.Adi == "xx");
            
            if(kul!=null)
            {
                kul.Adi = "Ömer";
                repo_kul.Save();
            }
        }

        public void DeleteTest()
        {
            Kullanici kul = repo_kul.Find(x => x.Adi == "Ömer");

            if(kul!=null)
            {
                repo_kul.Delete(kul);
            }
        }

        Repository<Yorum> rep_yorum = new Repository<Yorum>();
        Repository<Not> rep_not = new Repository<Not>();
        public void YorumTest()
        {
            Kullanici kul = repo_kul.Find(x => x.Id == 1);
            Not makale = rep_not.Find(x => x.Id == 3);

            Yorum y = new Yorum() {
             YorumText="Bu bir test yorumdur.",
             KayitTarihi=DateTime.Now,
             DegistirmeTarihi=DateTime.Now,
             DegistirenKullanici="elif",
             Kullanici=kul,
             Makale=makale
            };
            rep_yorum.Insert(y);
        }
    }
}
