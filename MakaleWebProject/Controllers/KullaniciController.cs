using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale.Entities;
using Makale.BusinessLayer;

namespace MakaleWebProject.Controllers
{
    public class KullaniciController : Controller
    {
        KullaniciYonet ky = new KullaniciYonet();
        
        public ActionResult Index()
        {
            return View(ky.KullaniciGetir());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           BusinessLayerResult<Kullanici> sonuc= ky.KullaniciBul(id.Value);
           
            if (sonuc.Sonuc == null)
            {
                return HttpNotFound();
            }
            return View(sonuc.Sonuc);
        }

        public ActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel kullanici)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanici> result = ky.KullaniciKaydet(kullanici);

                if (result.hata.Count > 0)
                {
                    result.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(kullanici);
                }

                return RedirectToAction("Index");
            }

            return View(kullanici);
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BusinessLayerResult<Kullanici> sonuc = ky.KullaniciBul(id.Value);

            if (sonuc.Sonuc == null)
            {
                return HttpNotFound();
            }
            return View(sonuc.Sonuc);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanici> sonuc = ky.KullaniciUpdate(kullanici);

                return RedirectToAction("Index");
            }
            return View(kullanici);
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessLayerResult<Kullanici> sonuc = ky.KullaniciBul(id.Value);

            if (sonuc.Sonuc == null)
            {
                return HttpNotFound();
            }
            return View(sonuc.Sonuc);
        }
               
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             BusinessLayerResult<Kullanici> sonuc = ky.KullaniciSil(id);

            return RedirectToAction("Index");
        }
 
    }
}
