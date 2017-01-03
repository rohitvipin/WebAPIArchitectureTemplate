using System.Net;
using System.Web.Http;
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
            var blogEntity = _blogService.GetById(id);
            return blogEntity != null ? (IHttpActionResult)Ok(blogEntity) : NotFound();
        }

        [HttpPost]
        public IHttpActionResult Save(BlogEntity blogEntity)
        {
            if (string.IsNullOrWhiteSpace(blogEntity?.Name))
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }

            if (_blogService.GetByName(blogEntity.Name) != null)
            {
                return StatusCode(HttpStatusCode.Conflict);
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
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            return Ok(blog);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
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
            return StatusCode(HttpStatusCode.NotAcceptable);
        }
    }
}
