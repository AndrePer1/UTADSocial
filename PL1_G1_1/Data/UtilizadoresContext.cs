using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PL1_G1.Models;

namespace PL1_G1.Data
{
    public class UtilizadoresContext : DbContext
    {
        public UtilizadoresContext (DbContextOptions<UtilizadoresContext> options)
            : base(options)
        {
        }

        public DbSet<PL1_G1.Models.User> User { get; set; } = default!;
    }
}
