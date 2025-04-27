using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Required field Username
            builder
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            // Username uniqueness
            builder
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}