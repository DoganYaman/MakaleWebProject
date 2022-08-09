using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale.BusinessLayer;
using Makale.Entities;
using MakaleWebProject.Filter;
using MakaleWebProject.Models;


namespace MakaleWebProject.Controllers
{
    [Auth][AuthAdmin]
    [Exc]
    public class KategoriController : Controller
    {
        KategoriYonet ky = new KategoriYonet(); 
        public ActionResult Index()
        {
            return View(ky.KategoriGetir());
        }
     
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

      
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategori kategori)
        {
            ModelState.Remove("KayitTarihi");
            ModelState.Remove("DegistirmeTarihi");
            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
               BusinessLayerResult<Kategori> sonuc=  ky.KategoriKaydet(kategori);

                if(sonuc.hata.Count>0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(kategori);
                }

                CacheHelper.CahceTemizle();
                return RedirectToAction("Index");
            }

            return View(kategori);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kategori kategori = ky.KategoriBul(id.Value);

            if (kategori == null)
            {
                return HttpNotFound();
            }

            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategori kategori)
        {
            ModelState.Remove("KayitTarihi");
            ModelState.Remove("DegistirmeTarihi");
            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kategori> sonuc = ky.KategoriUpdate(kategori);

                if (sonuc.hata.Count > 0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));

                    return View(kategori);
                }


                CacheHelper.CahceTemizle();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.KategoriBul(id.Value);
           
            if (kategori == null)
            {
                return HttpNotFound();
            }

            return View(kategori);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = ky.KategoriBul(id);

            BusinessLayerResult<Kategori> sonuc = ky.KategoriSil(kategori.Id);
            if (sonuc.hata.Count > 0)
            {
                return View(kategori);
            }


            CacheHelper.CahceTemizle();
            return RedirectToAction("Index");
        }

    }
}
