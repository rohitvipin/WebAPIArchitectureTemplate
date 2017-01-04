using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPIArchitectureTemplate.Database.Models;
using WebAPIArchitectureTemplate.DAL.Implementation;
using WebAPIArchitectureTemplate.DAL.Interfaces;
using WebAPIArchitectureTemplate.Services.Entities;
using WebAPIArchitectureTemplate.Services.Interfaces;

namespace WebAPIArchitectureTemplate.Services.Implementations
{
    public class BlogService : IBlogService
    {
        public BlogService()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Blog, BlogEntity>().ForMember(m => m.Id, opt => opt.MapFrom(b => b.BlogId));
            });
        }

        public IList<BlogEntity> GetAll()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return Mapper.Map<IList<Blog>, IList<BlogEntity>>(unitOfWork.BlogRepository.GetAll());
            }
        }

        public BlogEntity GetById(object id)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                if (unitOfWork.BlogRepository.Exists(id))
                {
                    return Mapper.Map<Blog, BlogEntity>(unitOfWork.BlogRepository.GetById(id));
                }
            }
            return null;
        }

        public void Insert(BlogEntity blogEntity)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.BlogRepository.Insert(new Blog
                {
                    Name = blogEntity.Name
                });

                unitOfWork.Save();
            }
        }

        public void Update(BlogEntity blogEntity)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.BlogRepository.Update(unitOfWork.BlogRepository.GetById(blogEntity.Id));

                unitOfWork.Save();
            }
        }

        public BlogEntity GetByName(string blogEntityName)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var blog = unitOfWork.BlogRepository.Get(x => x.Name == blogEntityName)?.FirstOrDefault();
                return blog != null ? Mapper.Map<Blog, BlogEntity>(blog) : null;
            }
        }

        public void Delete(int id)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.BlogRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
