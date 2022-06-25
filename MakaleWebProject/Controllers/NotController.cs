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
    [Exc]
    public class NotController : Controller
    {
        MakaleYonet my = new MakaleYonet();
        KategoriYonet ky = new KategoriYonet();
       
       
        [Auth]
        public ActionResult Index()
        {
            //var nots = my.MakaleGetir();

            //db.Nots.Include(n => n.Kategori);
            Kullanici user =(Kullanici)Session["login"];

            var nots = my.ListQueryable().Include("Kullanici").Where(x => x.Kullanici.Id == user.Id).OrderByDescending(x => x.DegistirmeTarihi);

            return View(nots.ToList());
        }

        public ActionResult Begendiklerim()
        {
            LikeYonet ly = new LikeYonet();

            Kullanici user = Session["login"] as Kullanici;

           var makale= ly.ListQueryable().Include("Kullanici").Include("Makale").Where(x => x.Kullanici.Id == user.Id).Select(x => x.Makale).Include("Kategori").Include("Kullanici").OrderByDescending(x => x.DegistirmeTarihi);
             
            return View("Index",makale.ToList());

        }

        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Not not = my.NotBul(id.Value);

            if (not == null)
            {
                return HttpNotFound();
            }
            return View(not);
        }

        [Auth]
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(CacheHelper.KategoriCahce(), "Id", "Baslik");
            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Not not)
        {
            ModelState.Remove("KayitTarihi");
            ModelState.Remove("DegistirmeTarihi");
            ModelState.Remove("DegistirenKullanici");

            ViewBag.KategoriId = new SelectList(CacheHelper.KategoriCahce(), "Id", "Baslik", not.KategoriId);

            if (ModelState.IsValid)
            {
                not.Kullanici =(Kullanici)Session["login"];

                 BusinessLayerResult<Not> sonuc= my.NotKaydet(not);

                if (sonuc.hata.Count > 0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(not);
                }

                return RedirectToAction("Index");
            }

          
            return View(not);
        }

        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Not not = my.NotBul(id.Value);
            if (not == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(CacheHelper.KategoriCahce(), "Id", "Baslik", not.KategoriId);
            return View(not);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Not not)
        {
            ModelState.Remove("KayitTarihi");
            ModelState.Remove("DegistirmeTarihi");
            ModelState.Remove("DegistirenKullanici");

            ViewBag.KategoriId = new SelectList(CacheHelper.KategoriCahce(), "Id", "Baslik", not.KategoriId);

            if (ModelState.IsValid)
            {
                not.Kullanici = (Kullanici)Session["login"];

                BusinessLayerResult<Not> sonuc = my.NotUpdate(not);

                if (sonuc.hata.Count > 0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(not);
                }                             

                return RedirectToAction("Index");
            }
          
            return View(not);
        }

        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Not not = my.NotBul(id.Value);
            if (not == null)
            {
                return HttpNotFound();
            }
            return View(not);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Auth]
        public ActionResult DeleteConfirmed(int id)
        {
           BusinessLayerResult<Not> sonuc= my.NotSil(id);

           return RedirectToAction("Index");
        }

        LikeYonet ly = new LikeYonet();
        public ActionResult LikeMakale(int[] dizi)
        {          

            Kullanici user=Session["login"] as Kullanici;

            List<int> likedizi = new List<int>();

            if(user!=null)
            {
                if (dizi != null)
                {
                    likedizi = ly.List(x => x.Kullanici.Id == user.Id && dizi.Contains(x.Makale.Id)).Select(x => x.Makale.Id).ToList();
                }
            }
                   
            
            //Select MakaleID
            //from Begeni b
            //where b.kullanıcıID=user.Id and 
            //b.makaleid in(3,5,9)

            return Json(new { sonuc =likedizi});

        }

        [HttpPost]
        public ActionResult LikeDurumuUpdate(int notid,bool like)
        {
            int sonuc = 0;
            Kullanici user = Session["login"] as Kullanici;

            Begeni begeni = ly.BegeniGetir(notid,user.Id);
            
            Not makale = my.NotBul(notid);

            if(begeni!=null && like==false)
            {
              sonuc=ly.BegeniSil(begeni);
            }
            else if(begeni==null && like==true)
            {
              sonuc=ly.BegeniEkle(new Begeni()
                { 
                    Kullanici=user,
                    Makale=makale

                });
            }

            if(sonuc>0)
            {
                if(like)
                {
                    makale.BegeniSayisi++;
                }
                else
                {
                    makale.BegeniSayisi--;
                }
               
               BusinessLayerResult<Not> result=  my.NotUpdate(makale);

                if (result.hata.Count == 0)
                {
                    return Json(new { hata = false, sonuc = makale.BegeniSayisi });
                }                   
            }

            return Json(new { hata = true, sonuc = makale.BegeniSayisi });

        }
    }
}
