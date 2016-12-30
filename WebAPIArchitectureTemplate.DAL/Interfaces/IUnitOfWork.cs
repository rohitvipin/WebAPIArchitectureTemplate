using System;
using WebAPIArchitectureTemplate.Database.Models;

namespace WebAPIArchitectureTemplate.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Blog> BlogRepository { get; }

        IGenericRepository<Post> PostRepository { get; }

        void Save();
    }
}