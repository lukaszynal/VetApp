using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAppDal.Repo
{
    public class VetAppDbContext : DbContext
    {
        public VetAppDbContext(DbContextOptions<VetAppDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Animal> Animals { get; set; }
    }
}
