using NLog;
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
        private readonly DbContext dbContext;
        private readonly DbSet<T> dbSet;

        private ILogger logger;

        public GeneralRepository(DbContext newDbContext, ILogger newLogger)
        {
            if (newDbContext == null)
                throw new ArgumentNullException("dbContext");
            else
            {
                dbContext = newDbContext;
                dbSet = dbContext.Set<T>();
                logger = newLogger;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return dbSet.AsNoTracking();
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
                return dbSet.Where(predicate);
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
                return dbSet.Find(id);
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
                dbSet.AddOrUpdate(entity);

                dbContext.SaveChanges();
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
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.SaveChanges();
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
                var deleteItem = dbSet.Find(id);

                if (deleteItem != null)
                { 
                    dbSet.Remove(deleteItem);
                    dbContext.SaveChanges();
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