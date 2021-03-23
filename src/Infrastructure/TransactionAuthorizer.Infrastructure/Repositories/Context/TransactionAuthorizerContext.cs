using Microsoft.EntityFrameworkCore;
using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Infrastructure.Repositories.Context
{
    public class TransactionAuthorizerContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public TransactionAuthorizerContext(DbContextOptions<TransactionAuthorizerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Account
            modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Account>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne()
                .HasForeignKey(t => t.Id);
            #endregion

            #region Transaction
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany();
            #endregion
        }
    }
}