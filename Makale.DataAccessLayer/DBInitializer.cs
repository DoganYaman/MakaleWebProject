using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Makale.Entities;

namespace Makale.DataAccessLayer
{
    public class DBInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Kullanici admin = new Kullanici()
            {
                Adi = "Elif",
                Soyadi = "Cengiz",
                Email = "cenelif@gmail.com",
                Aktif = true,
                Admin = true,
                KullaniciAdi = "elif",
                Sifre = "1234",
                AktifGuid = Guid.NewGuid(),
                KayitTarihi = DateTime.Now,
                ProfilResmi = "user_image.png",
                DegistirmeTarihi =DateTime.Now.AddMinutes(5),
                DegistirenKullanici="elif"       
            
            };
            context.Kullanicilar.Add(admin);
            context.SaveChanges();

            for (int i = 1; i < 10; i++)
            {
                Kullanici user = new Kullanici()
                {
                    Adi = FakeData.NameData.GetFirstName(),
                    Soyadi = FakeData.NameData.GetSurname(),
                    Email=FakeData.NetworkData.GetEmail(),
                    AktifGuid=Guid.NewGuid(),
                    Aktif=true,
                    Admin=false,
                    KullaniciAdi=string.Format("user{0}", i),
                    Sifre="123",                    KayitTarihi=DateTime.Now.AddDays(-1),
                    ProfilResmi = "user_image.png",
                DegistirmeTarihi =DateTime.Now,
                    DegistirenKullanici = string.Format("user{0}", i)
                };
              
                context.Kullanicilar.Add(user);        
            }
            context.SaveChanges();

            List<Kullanici> klist = context.Kullanicilar.ToList();

            //Kategori verileri ekleniyor
            for (int i = 0; i < 10; i++)
            {
                Kategori kat = new Kategori()
                {
                    Baslik=FakeData.PlaceData.GetStreetName(),
                    Aciklama=FakeData.PlaceData.GetAddress(),
                     KayitTarihi=DateTime.Now,
                     DegistirmeTarihi=DateTime.Now,
                     DegistirenKullanici="elif"
                };

                context.Kategoriler.Add(kat);

                //Kategoriye makale ekleniyor

                for (int j = 0; j < 6; j++)
                {
                    Not not = new Not()
                    {
                        Baslik=FakeData.NameData.GetCompanyName(),
                        Icerik=FakeData.TextData.GetSentences(3),
                         Taslak=false,
                         BegeniSayisi=FakeData.NumberData.GetNumber(1,9),
Kategori=kat,
KayitTarihi= DateTime.Now.AddDays(-2),
DegistirmeTarihi= DateTime.Now,
Kullanici= klist[j],
 DegistirenKullanici= klist[j].KullaniciAdi

                    };
                    
                    kat.Makaleler.Add(not);


                    //Makaleye yorum ekleniyor.
                    for (int k = 0; k < 3; k++)
                    {
                        Yorum y = new Yorum()
                        {
                            YorumText=FakeData.TextData.GetSentence(),
                            KayitTarihi=DateTime.Now,
                            DegistirmeTarihi=DateTime.Now,
                            Kullanici=klist[FakeData.NumberData.GetNumber(1,9)],
                            DegistirenKullanici=klist[FakeData.NumberData.GetNumber(1, 9)].KullaniciAdi
                        };

                        not.Yorumlar.Add(y);
                    }

                    //Makaleye beğeni ekleniyor.

                    for (int x = 0; x < not.BegeniSayisi; x++)
                    {
                        Begeni b = new Begeni() { 
                        
                             Kullanici=klist[FakeData.NumberData.GetNumber(1, 9)],
                             Makale=not

                        };

                        not.Begeniler.Add(b);
                    }

                }

            }

            context.SaveChanges();


        }
    }
}
