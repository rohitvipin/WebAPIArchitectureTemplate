using System.Collections.Generic;

namespace WebAPIArchitectureTemplate.Services.Entities
{
    public class BlogEntity
    {
        public string Name { get; set; }
        public IList<PostEntity> Posts { get; set; }
    }
}