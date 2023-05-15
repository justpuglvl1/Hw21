using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Test.Models;

namespace Test.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Author> Author { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}