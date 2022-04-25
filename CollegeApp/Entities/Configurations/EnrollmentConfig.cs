﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configurations
{
    public class EnrollmentConfig
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.Property(s => s.Active).HasDefaultValue(true);
        }
    }
}
