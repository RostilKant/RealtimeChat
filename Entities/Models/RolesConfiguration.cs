using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Models
{
    public class RolesConfiguration: IEntityTypeConfiguration<IdentityRole>
        {
            public void Configure(EntityTypeBuilder<IdentityRole> builder)
            {
                builder.HasData(new List<IdentityRole>
                {
                    new IdentityRole("User"),
                    new IdentityRole("Admin")
                }); 
            }
    }
}