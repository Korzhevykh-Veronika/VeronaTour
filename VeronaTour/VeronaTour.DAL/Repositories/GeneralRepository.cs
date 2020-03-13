using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.DAL.Repositories
{
    public class GeneralRepository<T> : IRepository<T> where T : class
    {
        private DbContext DbContext { get; set; }
        private DbSet<T> DbSet { get; set; }

        public GeneralRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            else
            {
                DbContext = dbContext;
                DbSet = DbContext.Set<T>();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IEnumerable<T> Find(Func<T, Boolean> predicate)
        {
            return DbSet.Where(predicate);
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Create(T entity)
        {
            DbSet.AddOrUpdate(entity);

            DbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            DbContext.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }

            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var deleteItem = DbSet.Find(id);
            if (deleteItem != null)
            { 
                DbSet.Remove(deleteItem);
                DbContext.SaveChanges();
            }
        }
    }
}