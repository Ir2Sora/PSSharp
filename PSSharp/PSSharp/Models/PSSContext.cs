using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PSSharp.Models
{
    public class PSSContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Direction> Directions { get; set; }
    }
}