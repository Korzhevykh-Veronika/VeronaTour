using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.DAL.Repositories
{
    public class GeneralRepository<T> : IRepository<T> where T : class
    {
        private DbContext DbContext { get; set; }
        private DbSet<T> DbSet { get; set; }

        private ILogger logger;

        public GeneralRepository(DbContext dbContext, ILogger newLogger)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            else
            {
                DbContext = dbContext;
                DbSet = DbContext.Set<T>();
                logger = newLogger;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return DbSet.AsNoTracking();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "SQL error");
            }

            return null;
        }

        public IEnumerable<T> Find(Func<T, Boolean> predicate)
        {
            try
            {
                return DbSet.Where(predicate);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "SQL error");
            }

            return null;
        }

        public T Get(int id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "SQL error");
            }

            return null;
        }

        public void Create(T entity)
        {
            try
            {
                DbSet.AddOrUpdate(entity);

                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SQL error");
            }
        }

        public void Update(T entity)
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "SQL error");
            }            
        }

        public void Update(IEnumerable<T> entities)
        {
            try
            { 
                foreach (var entity in entities)
                {
                    DbContext.Entry(entity).State = EntityState.Modified;
                }

                DbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "SQL error");
            }           
        }

        public void Delete(int id)
        {
            try
            {
                var deleteItem = DbSet.Find(id);

                if (deleteItem != null)
                { 
                    DbSet.Remove(deleteItem);
                    DbContext.SaveChanges();
                }
                else
                {
                    logger.Warn($"Item {typeof(T)} with id: {id} was not found.");
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "SQL error");
            }
        }
    }
}