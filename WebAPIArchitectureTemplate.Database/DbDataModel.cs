using WebAPIArchitectureTemplate.Database.Models;

namespace WebAPIArchitectureTemplate.Database
{
    using System.Data.Entity;

    public class DbDataModel : DbContext
    {
        // Your context has been configured to use a 'DbDataModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebAPIArchitectureTemplate.Database.DbDataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DbDataModel' 
        // connection string in the application configuration file.
        public DbDataModel()
            : base("name=DbDataModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Blog> BlogEntities { get; set; }
        public virtual DbSet<Post> PostEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}