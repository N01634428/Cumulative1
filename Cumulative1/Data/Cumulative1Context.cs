using System;


using Microsoft.EntityFrameworkCore;
using Cumulative1.Models;

namespace Cumulative1.Data
{
    public class Cumulative1Context : DbContext
    {
        public Cumulative1Context(DbContextOptions<Cumulative1Context> options)
            : base(options)
        {
        }

        public DbSet<Cumulative1.Models.Teacher> Teacher { get; set; } = default!;
    }
}