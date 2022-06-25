using Makale.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Makale.DataAccessLayer
{
   public class Repository<T> where T:class
    {
        private DatabaseContext db; 

        private DbSet<T> objset;

        public Repository()
        {
            db = Singleton.CreateContext();
            objset = db.Set<T>();
        }

        public List<T> List()
        {
            return objset.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return objset.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T,bool>> where )
        {
          return  objset.Where(where).ToList();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return objset.FirstOrDefault(where);
        }


        public int Insert(T nesne)
        {           

            if(nesne is EntityBase)
            {
                EntityBase obj =nesne as EntityBase; 
                obj.KayitTarihi = DateTime.Now;
                obj.DegistirmeTarihi = DateTime.Now;

                if(obj.DegistirenKullanici==null)
                 obj.DegistirenKullanici = "system";
            }

            objset.Add(nesne);
            return Save();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(T nesne)
        {
            if (nesne is EntityBase)
            {
                EntityBase obj = nesne as EntityBase;
                obj.DegistirmeTarihi = DateTime.Now;

                if (obj.DegistirenKullanici == null)
                    obj.DegistirenKullanici = "system";
            }
            return Save();
        }

        public int Delete(T nesne)
        {
            objset.Remove(nesne);
            return Save();
        }

    }
}
