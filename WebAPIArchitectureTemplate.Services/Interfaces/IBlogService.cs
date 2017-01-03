using System.Collections.Generic;
using WebAPIArchitectureTemplate.Services.Entities;

namespace WebAPIArchitectureTemplate.Services.Interfaces
{
    public interface IBlogService
    {
        IList<BlogEntity> GetAll();
        BlogEntity GetById(object id);
        void Insert(BlogEntity blogEntity);
        void Update(BlogEntity blogEntity);
        BlogEntity GetByName(string blogEntityName);
        void Delete(int id);
    }
}