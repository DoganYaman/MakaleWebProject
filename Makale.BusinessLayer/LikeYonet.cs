using Makale.DataAccessLayer;
using Makale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Makale.BusinessLayer
{
    public class LikeYonet
    {
        Repository<Begeni> rep_begeni = new Repository<Begeni>();

        public Begeni BegeniGetir(int notid, int userid)
        {
            return rep_begeni.Find(x => x.Makale.Id == notid && x.Kullanici.Id == userid);
        }

        public IQueryable<Begeni> ListQueryable()
        {
            return rep_begeni.ListQueryable();
        }

        public List<Begeni> List(Expression<Func<Begeni, bool>> kosul)
        {
            return rep_begeni.List(kosul);
        }

        public int BegeniEkle(Begeni begeni)
        {
            return rep_begeni.Insert(begeni);
        }

        public int BegeniSil(Begeni begeni)
        {
            return rep_begeni.Delete(begeni);
        }

    }
}
