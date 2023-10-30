using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(k => k.UserWallet)
                .WithOne(k => k.User)
               .HasForeignKey<Wallet>(k => k.WalletId)
             .IsRequired();

            modelBuilder.Entity<Wallet>()
               .HasMany(k => k.UserLoanRequest)
               .WithOne(k => k.Wallet)
              .HasForeignKey(k => k.WalletId)
            .IsRequired();

            modelBuilder.Entity<Wallet>()
                .HasMany(e => e.WalletTransaction)
                .WithOne(e => e.Wallet)
                .HasForeignKey(e => e.WalletId)
                .IsRequired();
        }

        
    }
}
