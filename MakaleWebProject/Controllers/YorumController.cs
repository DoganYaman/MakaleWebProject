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
    public class YorumController : Controller
    {
        // GET: Yorum
        public ActionResult YorumGoster(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakaleYonet my = new MakaleYonet();

            Not not = my.NotBul(id.Value);
            
            if(not==null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialYorum",not.Yorumlar);
        }

        YorumYonet yy = new YorumYonet();

        [Auth]
        [HttpPost]
        public ActionResult YorumUpdate(int? id,string text)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Yorum yorum=yy.YorumBul(id.Value);

            if(yorum==null)
            {
                return new HttpNotFoundResult();
            }

            yorum.YorumText = text;

           if(yy.YorumUpdate(yorum)>0)
            {
                return Json(new { sonuc = true });
            }

           return Json(new { sonuc = false });

        }

        [Auth]
        public ActionResult YorumSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Yorum yorum = yy.YorumBul(id.Value);

            if (yorum == null)
            {
                return new HttpNotFoundResult();
            }

            if (yy.YorumSil(yorum) > 0)
            {
                return Json(new { sonuc = true },JsonRequestBehavior.AllowGet);
            }

            return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);

        }

        MakaleYonet my = new MakaleYonet();

        [Auth]
        [HttpPost]
        public ActionResult YorumEkle(Yorum yorum,int? notid)
        {
            if (notid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Not not = my.NotBul(notid.Value);

            if (not == null)
            {
                return new HttpNotFoundResult();
            }

            yorum.Makale = not;
            yorum.Kullanici = Session["login"] as Kullanici;

            if(yy.YorumEkle(yorum)>0)
            {
                return Json(new { sonuc = true });
            }

            return Json(new { sonuc = false });

        }

        //[HttpPost]
        //public ActionResult YorumSil(int? id,string text)
        //{
        //    return Json(new { sonuc = false };
        //}
    }
}