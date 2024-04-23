using Palyavalaszto.Data.Entitites;
using Microsoft.EntityFrameworkCore;
namespace Palyavalaszto.Data.Entitites
{
    public class MyWorldDbContext:DbContext
    {
        public MyWorldDbContext(DbContextOptions<MyWorldDbContext> options):base(options)
        {

        }
        public DbSet<user> users{get;set;}
         public DbSet<applications>  application { get;set; }
        public DbSet<role> Roles { get; set; }
        public DbSet<job> Jobs { get; set; }
        public DbSet<employee> Employees { get; set; }
        public DbSet<employer> Employers { get; set; }
    }
}
