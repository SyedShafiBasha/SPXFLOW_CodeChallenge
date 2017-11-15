#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

#endregion

namespace DataModel.GenericRepository
{
    public class Repository<TEntity> where TEntity : class
    {

        internal Product_DBEntities Context;
        internal DbSet<TEntity> DbSet;

        public Repository(Product_DBEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }


        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }


        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }


        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }


        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }


        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

    }
}