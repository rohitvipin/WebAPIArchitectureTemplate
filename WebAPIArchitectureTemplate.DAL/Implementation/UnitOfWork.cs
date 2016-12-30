using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using WebAPIArchitectureTemplate.Database;
using WebAPIArchitectureTemplate.Database.Models;
using WebAPIArchitectureTemplate.DAL.Interfaces;
using WebAPIArchitectureTemplate.Logging;

namespace WebAPIArchitectureTemplate.DAL.Implementation
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Variables

        private readonly DbDataModel _context;
        private IGenericRepository<Blog> _blogRepository;
        private IGenericRepository<Post> _postRepository;

        #endregion

        public UnitOfWork()
        {
            _context = new DbDataModel();
        }

        #region Public Properties

        public IGenericRepository<Blog> BlogRepository => _blogRepository ?? (_blogRepository = new GenericRepository<Blog>(_context));

        public IGenericRepository<Post> PostRepository => _postRepository ?? (_postRepository = new GenericRepository<Post>(_context));

        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputSb = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputSb.AppendLine($"{DateTime.Now}: Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:")
                            .AppendLine(string.Join(Environment.NewLine, eve.ValidationErrors.Select(ve => $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"")))
                            .AppendLine(Environment.NewLine);

                }

                ErrorLogger.LogMessage(outputSb.ToString());
                throw;
            }
        }

        #endregion

        #region IDisposible Implementation

        private bool _disposed;

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}