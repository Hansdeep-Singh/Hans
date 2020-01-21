using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vc.models
{
    public class ChartContext : DbContext
    {
        public virtual DbSet<Chart> Charts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Connection String Goes here");
        }

    }
}
