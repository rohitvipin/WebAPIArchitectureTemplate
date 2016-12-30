using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WebAPIArchitectureTemplate.Database;
using WebAPIArchitectureTemplate.DAL.Interfaces;

namespace WebAPIArchitectureTemplate.DAL.Implementation
{
    /// <summary>
    /// Generic Repository class for Entity Operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Private members
        internal DbDataModel Context;
        internal DbSet<TEntity> DbSet;
        #endregion

        #region Constructor
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DbDataModel context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity) => DbSet.Add(entity);

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id) => Delete(DbSet.Find(id));

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public void Delete(Func<TEntity, bool> where)
        {
            foreach (var obj in DbSet.Where(where))
            {
                DbSet.Remove(obj);
            }
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public virtual TEntity GetById(object id) => DbSet.Find(id);

        /// <summary>
        /// generic method to fetch all the records from db
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll() => DbSet.ToList();

        /// <summary>
        /// Get records based on conditions and by including other tables. 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, string[] include = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (include != null)
            {
                query = include.Aggregate(query, (current, inc) => current.Include(inc));
            }
            return query.Where(predicate);
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey) => DbSet.Find(primaryKey) != null;

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate) => DbSet.Single(predicate);

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public TEntity GetFirst(Func<TEntity, bool> predicate) => DbSet.First(predicate);

        #endregion
    }
}
