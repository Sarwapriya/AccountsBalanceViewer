using AccountsBalanceViewer.Domain.Entities;
using AccountsBalanceViewer.Persistance.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace AccountsBalanceViewer.Persistance
{
    public class AccountsBalanceViewerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Balance> Balances { get; set; }

        public AccountsBalanceViewerContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new UserRoleEntityTypeConfiguration().Configure(modelBuilder.Entity<UserRole>());
            new AcoountTypeEntityTypeConfiguration().Configure(modelBuilder.Entity<AccountType>());
            new BalanceEntityTypeConfiguration().Configure(modelBuilder.Entity<Balance>());
        }
    }
}