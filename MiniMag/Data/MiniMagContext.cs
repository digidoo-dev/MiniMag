using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniMag.Models;

namespace MiniMag.Data
{
    public class MiniMagContext : DbContext
    {
        public MiniMagContext (DbContextOptions<MiniMagContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Supplier> Supplier { get; set; } = default!;
        public DbSet<Intake> Intake { get; set; } = default!;
        public DbSet<Issue> Issue { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
