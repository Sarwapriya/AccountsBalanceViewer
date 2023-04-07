using AccountsBalanceViewer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountsBalanceViewer.Persistance.EntityConfiguration
{
    public class AcoountTypeEntityTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable("AcoountType")
                .HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired(true);
            builder.Property(u => u.MinimumBalance)
                .IsRequired(true);

        }
    }
}
