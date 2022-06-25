using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makale.Common;
using Makale.DataAccessLayer;
using Makale.Entities;

namespace Makale.BusinessLayer
{
    public class KullaniciYonet
    {
        private Repository<Kullanici> repo_kul = new Repository<Kullanici>();

        BusinessLayerResult<Kullanici> kul_sonuc = new BusinessLayerResult<Kullanici>();
        public List<Kullanici> KullaniciGetir()
        {
            return repo_kul.List();
        }

        public BusinessLayerResult<Kullanici>  KullaniciKaydet(RegisterViewModel model)
        {
            //Kullanıcıadı ve eposta kontrolu
            //Kayıt işlemi
            //Aktivasyon maili gönder
           Kullanici kul= repo_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi || x.Email == model.Email);
     
            if(kul!=null)
            {
                if(kul.KullaniciAdi==model.KullaniciAdi)
                {
                    kul_sonuc.hata.Add("Kullanıcı adı kayıtlı");
                }
                if(kul.Email==model.Email)
                {
                    kul_sonuc.hata.Add("Eposta adresi kayıtlı.");
                }
            }
            else
            {
                int sonuc = repo_kul.Insert(new Kullanici()  {  KullaniciAdi=model.KullaniciAdi,
                    Email=model.Email,
                    Sifre=model.Sifre,
                    AktifGuid=Guid.NewGuid(),
                    Aktif=false,
                    Admin=false
                });

                if(sonuc>0)
                {
                    kul_sonuc.Sonuc = repo_kul.Find(x => x.Email == model.Email && x.KullaniciAdi == model.KullaniciAdi);

                    string siteUrl = ConfigHelper.Get<string>("SiteRootUrl");

                    string activateURL = string.Format("{0}/Home/UserActivate/{1}", siteUrl, kul_sonuc.Sonuc.AktifGuid);

                    string body = string.Format("Merhaba{0}{1} <br> Hesabınızı aktifleştirmek için, <a href='{2}' target='_blank'> tıklayınız </a>", kul_sonuc.Sonuc.Adi, kul_sonuc.Sonuc.Soyadi, activateURL);   //_blank :yeni sekmede aç
                    
                    
                    MailHelper.SendMail(body, kul_sonuc.Sonuc.Email, "Hesap Aktifleştirme");

                    //Aktivasyon maili gönderilecek
                }

            }

            return kul_sonuc;

        }
    
        public BusinessLayerResult<Kullanici> LoginKullanici(LoginViewModel model)
        {
            kul_sonuc.Sonuc = repo_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi && x.Sifre == model.Sifre);

            if(kul_sonuc.Sonuc!=null)
            {
               if(!kul_sonuc.Sonuc.Aktif)
                {
                    kul_sonuc.hata.Add("Kullanıcı aktifleştirilmemiştir. Lütfen e-postanızı kontrol ediniz.");
                }
            }
            else
            {
                kul_sonuc.hata.Add("Kullanıcı adı ya da şifre uyuşmuyor.");
            }
            return kul_sonuc;

        }

        public BusinessLayerResult<Kullanici> KullaniciBul(int id)
        {
           kul_sonuc.Sonuc=repo_kul.Find(x => x.Id == id);

            if(kul_sonuc.Sonuc==null)
            {
                kul_sonuc.hata.Add("Kullanıcı bulunamadı.");
            }
            return kul_sonuc;
        }

        public BusinessLayerResult<Kullanici> ActivateUser(Guid aktifGuid)
        {
            kul_sonuc.Sonuc = repo_kul.Find(x => x.AktifGuid == aktifGuid);

            if(kul_sonuc.Sonuc!=null)
            {
                if(kul_sonuc.Sonuc.Aktif)
                {
                    kul_sonuc.hata.Add("Kullanıcı zaten aktif edilmiştir.");
                    return kul_sonuc;
                }

                kul_sonuc.Sonuc.Aktif = true;
                repo_kul.Update(kul_sonuc.Sonuc);
            }
            return kul_sonuc;

        }

        public BusinessLayerResult<Kullanici> KullaniciUpdate(Kullanici model)
        {
            Kullanici user = repo_kul.Find(x=>x.Id!=model.Id &&(x.KullaniciAdi==model.KullaniciAdi || x.Email==model.Email));

            if(user!=null && user.Id!=model.Id)
            {
                if(user.KullaniciAdi==model.KullaniciAdi)
                {
                    kul_sonuc.hata.Add("Bu kullanıcı adı kayıtlı.");
                }

                if(user.Email==model.Email)
                {
                    kul_sonuc.hata.Add("Bu e-posta kayıtlı");
                }

                return kul_sonuc;
            }

            kul_sonuc.Sonuc = repo_kul.Find(x => x.Id == model.Id);

            kul_sonuc.Sonuc.Email = model.Email;
            kul_sonuc.Sonuc.Adi = model.Adi;
            kul_sonuc.Sonuc.Soyadi = model.Soyadi;
            kul_sonuc.Sonuc.KullaniciAdi = model.KullaniciAdi;
            kul_sonuc.Sonuc.Sifre = model.Sifre;

            if (string.IsNullOrEmpty(model.ProfilResmi) == false)
                kul_sonuc.Sonuc.ProfilResmi = model.ProfilResmi;

            if (repo_kul.Update(kul_sonuc.Sonuc) == 0)
                kul_sonuc.hata.Add("Kullanıcı güncellenemedi.");

            return kul_sonuc;
        }

        public BusinessLayerResult<Kullanici> KullaniciSil(int id)
        {
            Kullanici user = repo_kul.Find(x => x.Id == id);

            if(user==null)
            {
                kul_sonuc.hata.Add("Kullanıcı bulunamadı.");
            }
            else
            {
                if (repo_kul.Delete(user) == 0)
                {
                  kul_sonuc.hata.Add("Kullanıcı silinemedi.");                    
                }
                   
            }
            return kul_sonuc;

        }
    }
}
