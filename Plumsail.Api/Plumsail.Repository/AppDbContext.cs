using Microsoft.EntityFrameworkCore;
using Plumsail.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumsail.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Something> Somethings { get; set; }
        public DbSet<SomethingType> SomethingTypes { get; set; }
    }
}
