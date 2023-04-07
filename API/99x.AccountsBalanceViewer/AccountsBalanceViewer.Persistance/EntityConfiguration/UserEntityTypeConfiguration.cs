using AccountsBalanceViewer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Persistance.EntityConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User")
                .HasKey(u=> u.Id);

            builder.HasIndex(u=>u.Email)
                .IsUnique();
            builder.Property(u => u.Name)
                .IsRequired(true);
            builder.Property(u=>u.RoleId)
                .IsRequired(true);
            builder
                .HasOne(u => u.UserRoles)
                .WithMany(u=> u.Users);

        }
    }
}
