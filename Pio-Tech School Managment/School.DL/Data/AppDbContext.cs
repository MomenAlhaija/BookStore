
using School.DL.Model;
using System.Data.Entity;

namespace School.DL.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base("name=AppDbContextConnection")
        {

        }

        public DbSet<Class> Classes { get; set; }
    }
}
