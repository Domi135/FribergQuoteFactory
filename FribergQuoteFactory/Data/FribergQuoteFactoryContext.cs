using Microsoft.EntityFrameworkCore;

namespace FribergQuoteFactory.Data
{
    public class FribergQuoteFactoryContext: DbContext
    {
        public FribergQuoteFactoryContext()
        {
        }

        public FribergQuoteFactoryContext(DbContextOptions<FribergQuoteFactoryContext> options)
        :base(options)
        {
        }

        public DbSet<Models.Quote> Quotes { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Models.Quote>().HasKey(q => q.Id);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
