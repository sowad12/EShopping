using Discount.Library.Model.Entites;
using EShopping.Core.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;


namespace Discount.Library.Context
{
  
    public class DiscountApplicationDbContext : BaseApplicationDbContext<DiscountApplicationDbContext>
    {
        public DiscountApplicationDbContext() : base()
        {

        }

        public DiscountApplicationDbContext(DbContextOptions<DiscountApplicationDbContext> options) : base(options)
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

        public DbSet<Coupon> Coupons { get; set; }
    }
}
