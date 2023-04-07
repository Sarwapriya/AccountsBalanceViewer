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
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole")
                .HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired(true);

        }
    }
}
