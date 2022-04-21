using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasIndex(s => s.Code).IsUnique();
            builder.Property(s => s.Code).HasMaxLength(10);
            builder.Property(s => s.FirstName).HasMaxLength(50);
            builder.Property(s => s.LastName).HasMaxLength(50);
        }
    }
}
