using System.Collections.Generic;
using WebAPIArchitectureTemplate.Services.Entities;

namespace WebAPIArchitectureTemplate.Services.Interfaces
{
    public interface IBlogService
    {
        IList<BlogEntity> GetAll();
        BlogEntity GetById(object id);
    }
}