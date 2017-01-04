using System;
using System.Net;
using System.Web.Http;
using WebAPIArchitectureTemplate.Logging;
using WebAPIArchitectureTemplate.Services.Entities;
using WebAPIArchitectureTemplate.Services.Implementations;
using WebAPIArchitectureTemplate.Services.Interfaces;

namespace WebAPIArchitectureTemplate.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;

        public BlogController()
        {
            _blogService = new BlogService();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var blogEntity = _blogService.GetById(id);
                return blogEntity != null ? (IHttpActionResult)Ok(blogEntity)
                    : ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.NotFound, $"Blog with id {id} not found.");
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Save(BlogEntity blogEntity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(blogEntity?.Name))
                {
                    return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.ExpectationFailed, "Required value(s) can't be null.");
                }

                if (_blogService.GetByName(blogEntity.Name) != null)
                {
                    return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.Conflict, $"The name {blogEntity.Name} already exists in the database.");
                }

                if (_blogService.GetById(blogEntity.Id) == null)
                {
                    _blogService.Insert(blogEntity);
                }
                else
                {
                    _blogService.Update(blogEntity);
                }

                var blog = _blogService.GetByName(blogEntity.Name);

                if (blog == null)
                {
                    return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.NotAcceptable, "The save opertation did not succeed.");
                }
                return Ok(blog);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.ExpectationFailed, "Id can't be zero.");
                }

                var blogEntity = _blogService.GetById(id);
                if (blogEntity == null)
                {
                    return NotFound();
                }

                _blogService.Delete(id);

                if (_blogService.GetById(id) == null)
                {
                    return Ok($"Blog Id {id} deleted successfully");
                }
                return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.NotAcceptable, "Unable to delete.");
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return ActionResultHelper.StatusCodeWithMessage(HttpStatusCode.InternalServerError, exception.Message);
            }
        }
    }
}
