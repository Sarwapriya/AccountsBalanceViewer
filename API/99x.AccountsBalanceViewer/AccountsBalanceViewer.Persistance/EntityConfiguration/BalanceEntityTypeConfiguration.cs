using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountsBalanceViewer.Domain.Entities;

namespace AccountsBalanceViewer.Persistance.EntityConfiguration
{
    internal class BalanceEntityTypeConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.ToTable("Balance")
                .HasKey(b => b.Id);

            builder.HasIndex(b => b.AccountTypeId)
                .IsUnique();
            builder.Property(b => b.Month)
                .IsRequired(true);
            builder.Property(b => b.Year)
                .IsRequired(true);
            builder.Property(b => b.Amount)
                .IsRequired(true);
            builder
                .HasOne<AccountType>(b => b.AccountType)
                .WithMany(b => b.Balances)
                .HasForeignKey(b=> b.AccountTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
