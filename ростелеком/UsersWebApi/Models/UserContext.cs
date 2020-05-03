using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Objects;
using Microsoft.EntityFrameworkCore;

namespace UsersWebApi.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
       : base(options)
        {
        }

        public DbSet<User> UserItems { get; set; }
    }
}
