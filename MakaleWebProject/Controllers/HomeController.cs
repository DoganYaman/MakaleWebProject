using Makale.BusinessLayer;
using Makale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MakaleWebProject.Filter;


namespace MakaleWebProject.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
    
        MakaleYonet my = new MakaleYonet();
        KategoriYonet ky = new KategoriYonet();
        KullaniciYonet kuly = new KullaniciYonet();
        public ActionResult Index()
        {
            //return View(my.MakaleGetir().OrderByDescending(x=>x.DegistirmeTarihi).ToList());

            return View(my.ListQueryable().Where(x => x.Taslak == false).OrderByDescending(x => x.DegistirmeTarihi).ToList());
        }
             

        public ActionResult Kategori(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                       
            Kategori kat=ky.KategoriBul(id.Value);

            if(kat==null)
            {
                return HttpNotFound();
            }

            List<Not> makaleler = my.ListQueryable().Where(x => x.Taslak == false && x.KategoriId==id).OrderByDescending(x => x.DegistirmeTarihi).ToList();

            return View("Index",makaleler);
        }

        public ActionResult EnBegenilenler()
        {
            return View("Index", my.MakaleGetir().OrderByDescending(x => x.BegeniSayisi).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
              BusinessLayerResult<Kullanici> sonuc=  kuly.LoginKullanici(model);
             
                if(sonuc.hata.Count>0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                Session["login"] = sonuc.Sonuc;

                return RedirectToAction("Index");

            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
               BusinessLayerResult<Kullanici> result= kuly.KullaniciKaydet(model);

                if(result.hata.Count>0)
                {
                    result.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                return RedirectToAction("RegisterOK");
            }

            return View();
        }

        public ActionResult RegisterOK()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }


        [Auth]
        public ActionResult ProfilGoster()
        {
            Kullanici user = Session["login"] as Kullanici;
            BusinessLayerResult<Kullanici> result =kuly.KullaniciBul(user.Id);

            if(result.hata.Count>0)
            {
                //Hata sayfasına yönlenebilir.
            }

            return View(result.Sonuc);
        }

        [Auth]
        public ActionResult ProfilDuzenle()
        {
            Kullanici user = Session["login"] as Kullanici;
            BusinessLayerResult<Kullanici> result = kuly.KullaniciBul(user.Id);

            if (result.hata.Count > 0)
            {
                //Hata sayfasına yönlenebilir.
            }

            return View(result.Sonuc);
        }

        [Auth]
        [HttpPost]
        public ActionResult ProfilDuzenle(Kullanici model,HttpPostedFileBase profilresim)
        {

            ModelState.Remove("DegistirenKullanici");

            if(ModelState.IsValid)
            {
                if (profilresim != null && (profilresim.ContentType == "image/jpeg" || profilresim.ContentType == "image/jpg" ||
              profilresim.ContentType == "image/png"))
                {
                    string dosyaadi = string.Format("user_{0}.{1}", model.Id, profilresim.ContentType.Split('/')[1]);

                    profilresim.SaveAs(Server.MapPath(string.Format("~/image/{0}", dosyaadi)));
                    model.ProfilResmi = dosyaadi;
                }

                BusinessLayerResult<Kullanici> sonuc = kuly.KullaniciUpdate(model);

                if (sonuc.hata.Count > 0)
                {
                    for (int i = 0; i < sonuc.hata.Count; i++)
                    {
                      ModelState.AddModelError("", (sonuc.hata)[i]);
                    }
                    
                    return View(model);
                }

                Session["login"] = sonuc.Sonuc;

                return RedirectToAction("ProfilGoster");
            }

            return View(model);
          
        }

        [Auth]
        public ActionResult ProfilSil()
        {
            Kullanici user = Session["login"] as Kullanici;

            BusinessLayerResult<Kullanici> sonuc = kuly.KullaniciSil(user.Id);

            if (sonuc.hata.Count > 0)
                return RedirectToAction("ProfilGoster");


            Session.Clear();
            return RedirectToAction("Index");
        }

        [Auth]
        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<Kullanici> sonuc = kuly.ActivateUser(id);

            if(sonuc.hata.Count>0)
            {
                TempData["error"] = sonuc.hata;
                return RedirectToAction("UserActivateError");
            }

            return RedirectToAction("UserActivateOK");
        }

        public ActionResult UserActivateError()      {

            List<string> hatamesaj = null;

            if(TempData["error"]!=null)
               hatamesaj= TempData["error"] as List<string>;

            return View(hatamesaj);
        }
        public ActionResult UserActivateOK()
        {
            return View();
        }

        public ActionResult YetkisizErisim()
        {
            return View();
        }

        public ActionResult HataSayfasi()
        {
            return View();
        }
    }
}