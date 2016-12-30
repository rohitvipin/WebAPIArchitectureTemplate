using System.Web.Http;
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
        public IHttpActionResult Get(int id) => Ok(_blogService.GetById(id));
    }
}
