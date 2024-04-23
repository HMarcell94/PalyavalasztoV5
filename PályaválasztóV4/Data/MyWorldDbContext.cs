using Microsoft.EntityFrameworkCore;
using PalyavalsztoV4.Models.v4_1;

namespace PalyavalsztoV4.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<job> Jobs { get; set; }
    }
}
