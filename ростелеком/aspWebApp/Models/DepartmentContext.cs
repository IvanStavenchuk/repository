using Common.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentWepApi.Models
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext(DbContextOptions<DepartmentContext> options)
            : base(options)
        {
        }

        public DbSet<Department> DepartmentItems { get; set; }
    }
}
