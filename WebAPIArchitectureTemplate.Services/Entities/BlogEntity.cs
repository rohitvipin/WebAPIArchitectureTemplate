using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebAPIArchitectureTemplate.Services.Entities
{
    public class BlogEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("posts")]
        public IList<PostEntity> Posts { get; set; }
    }
}