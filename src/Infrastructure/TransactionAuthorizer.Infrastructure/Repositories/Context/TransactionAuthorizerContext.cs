using Microsoft.EntityFrameworkCore;
using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Infrastructure.Repositories.Context
{
    public class TransactionAuthorizerContext : DbContext
    {
        public TransactionAuthorizerContext(DbContextOptions<TransactionAuthorizerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Account
            modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Account>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
            #endregion

            #region Transaction
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            #endregion
        }
    }
}