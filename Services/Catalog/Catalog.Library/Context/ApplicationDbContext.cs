﻿

using EShopping.Core.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Library.Context
{
    public class ApplicationDbContext: BaseApplicationDbContext<ApplicationDbContext>
    {
        public ApplicationDbContext() : base()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Product> Product { get; set; }
    }
}
