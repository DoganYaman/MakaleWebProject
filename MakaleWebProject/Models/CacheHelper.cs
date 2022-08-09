using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Makale.Entities;
using System.Web.Helpers;
using Makale.BusinessLayer;

namespace MakaleWebProject.Models
{
    public class CacheHelper
    {
        public static List<Kategori> KategoriCahce()
        {
            var sonuc = WebCache.Get("kategori-cahce");

            if(sonuc==null)
            {
                KategoriYonet ky = new KategoriYonet();
                sonuc = ky.KategoriGetir();

                WebCache.Set("kategori-cahce", sonuc, 20, true);
            }

            return sonuc;
        }

        public static void CahceTemizle()
        {
            WebCache.Remove("kategori-cahce");
        }

    }
}