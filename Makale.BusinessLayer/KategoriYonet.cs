using Makale.DataAccessLayer;
using Makale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.BusinessLayer
{
    public class KategoriYonet
    {
        private Repository<Kategori> repo_kat = new Repository<Kategori>();
        public List<Kategori> KategoriGetir()
        {
            return repo_kat.List();
        }

        public Kategori KategoriBul(int id)
        {
            return repo_kat.Find(x => x.Id == id);
        }

        BusinessLayerResult<Kategori> kat_sonuc = new BusinessLayerResult<Kategori>();

        public BusinessLayerResult<Kategori> KategoriKaydet(Kategori model)
        {           
            Kategori kat = repo_kat.Find(x => x.Baslik == model.Baslik);

            if (kat != null)
            {              
                  kat_sonuc.hata.Add("Kategori adı kayıtlı");               
            }
            else
            {
                int sonuc = repo_kat.Insert(new Kategori()
                {
                     Baslik=model.Baslik,
                     Aciklama=model.Aciklama
                });
            }

            return kat_sonuc;

        }


        public BusinessLayerResult<Kategori> KategoriUpdate(Kategori model)
        {
            Kategori kat = repo_kat.Find(x=>x.Baslik==model.Baslik && x.Id!=model.Id);

            if(kat!=null)
            {
                kat_sonuc.hata.Add("Kategori adı kayıtlı");
            }
            else
            {
                kat_sonuc.Sonuc = repo_kat.Find(x => x.Id == model.Id);
                kat_sonuc.Sonuc.Baslik = model.Baslik;
                kat_sonuc.Sonuc.Aciklama = model.Aciklama;

               int sonuc=repo_kat.Update(kat_sonuc.Sonuc);

                if(sonuc>0)
                {
                    kat_sonuc.Sonuc = repo_kat.Find(x => x.Id == model.Id);
                }
            }

            return kat_sonuc;
        }
    
    

        public BusinessLayerResult<Kategori> KategoriSil(int id)
        {
            Kategori k = repo_kat.Find(x => x.Id == id);

            //kategorinin notlarını bul
            //notların yorumlarını bul sil
            //notların like bul sil
            //notu sil 
            //kategori sil

            if(k==null)
            {
                kat_sonuc.hata.Add("Kategori bulunamadı.");
            }

       
            int sonuc=repo_kat.Delete(k);

            return kat_sonuc;
        }
    
    }
}
