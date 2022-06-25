using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makale.DataAccessLayer;
using Makale.Entities;

namespace Makale.BusinessLayer
{
    public class MakaleYonet
    {
        private Repository<Not> repo_not = new Repository<Not>();

        BusinessLayerResult<Not> not_result = new BusinessLayerResult<Not>();

        public List<Not> MakaleGetir()
        {
            return repo_not.List();
        }

        public IQueryable<Not> ListQueryable()
        {
            return repo_not.ListQueryable();
        }

        public Not NotBul(int id)
        {
            return repo_not.Find(x => x.Id == id);
        }

        public BusinessLayerResult<Not> NotKaydet(Not not)
        {
            not_result.Sonuc = repo_not.Find(x => x.Baslik == not.Baslik && x.KategoriId == not.KategoriId);

                if(not_result.Sonuc!=null)
            {
                not_result.hata.Add("Bu makale kayıtlı.");
            }
                else
            {
                Not n = new Not();
                n.Kullanici = not.Kullanici;
                n.KategoriId = not.KategoriId;
                n.Baslik = not.Baslik;
                n.Icerik = not.Icerik;
                n.Taslak = not.Taslak;
                n.DegistirenKullanici = not.Kullanici.KullaniciAdi;

                int sonuc=repo_not.Insert(n);
                if(sonuc==0)
                {
                    not_result.hata.Add("Makale kaydedilemedi.");
                }
                else
                {
                    not_result.Sonuc = n;
                }

               
            }


            return not_result;
            
        }

        public BusinessLayerResult<Not> NotUpdate(Not not)
        {
            not_result.Sonuc = repo_not.Find(x => x.Baslik == not.Baslik && x.KategoriId == not.KategoriId && x.Id!=not.Id);

            if(not_result.Sonuc!=null)
            {
                not_result.hata.Add("Bu makale kayıtlı");
            }
            else 
            {
                not_result.Sonuc = repo_not.Find(x => x.Id == not.Id);
                not_result.Sonuc.KategoriId = not.KategoriId;
                not_result.Sonuc.Baslik = not.Baslik;
                not_result.Sonuc.Icerik = not.Icerik;
                not_result.Sonuc.Taslak = not.Taslak;
                not_result.Sonuc.DegistirenKullanici = not.Kullanici.KullaniciAdi;

               int sonuc= repo_not.Update(not_result.Sonuc);

                if (sonuc == 0)
                {
                    not_result.hata.Add("Makale değiştirilemedi.");
                }
                else
                    not_result.Sonuc= repo_not.Find(x => x.Id == not.Id);

            }

            return not_result;
        }

        public BusinessLayerResult<Not> NotSil(int id)
        {

            Not not = repo_not.Find(x => x.Id == id);
            if(not!=null)
            {
               int sonuc=repo_not.Delete(not);
                if(sonuc==0)
                {
                    not_result.hata.Add("Makale silinemedi");
                }
            }
            else
            {
                not_result.hata.Add("Makale bulunamadı.");
            }

            return not_result;

        }
    }
}
