using System.Collections.Generic;
using AutoMapper;
using WebAPIArchitectureTemplate.Database.Models;
using WebAPIArchitectureTemplate.DAL.Implementation;
using WebAPIArchitectureTemplate.Services.Entities;
using WebAPIArchitectureTemplate.Services.Interfaces;

namespace WebAPIArchitectureTemplate.Services.Implementations
{
    public class BlogService : IBlogService
    {
        public IList<BlogEntity> GetAll()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return Mapper.Map<IList<Blog>, IList<BlogEntity>>(unitOfWork.BlogRepository.GetAll());
            }
        }

        public BlogEntity GetById(object id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return Mapper.Map<Blog, BlogEntity>(unitOfWork.BlogRepository.GetById(id));
            }
        }

        public void Insert(BlogEntity blogEntity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.BlogRepository.Insert(new Blog
                {
                    Name = blogEntity.Name
                });

                unitOfWork.Save();
            }
        }
    }
}
