using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Test.API.Models
{
    public class DiaryContext : DbContext
    {
        public DbSet<Notes> Notes { get; set; }

        public DiaryContext(DbContextOptions<DiaryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
