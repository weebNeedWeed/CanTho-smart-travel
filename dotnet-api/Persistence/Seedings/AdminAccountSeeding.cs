using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seedings;
internal static class AdminAccountSeeding
{
    public static ModelBuilder SeedAdmin(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>()
            .HasData(
                new Admin
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                }
            );
        return modelBuilder;
    }
}
