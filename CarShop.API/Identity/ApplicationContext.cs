using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarShop.API.Identity
{
    public class ApplicationContext: IdentityDbContext<User>
    {
                public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }
        
    }
}