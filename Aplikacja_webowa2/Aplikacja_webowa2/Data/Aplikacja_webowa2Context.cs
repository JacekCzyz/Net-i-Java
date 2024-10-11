using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aplikacja_webowa2.Models;

namespace Aplikacja_webowa2.Data
{
    public class Aplikacja_webowa2Context : DbContext
    {
        public Aplikacja_webowa2Context (DbContextOptions<Aplikacja_webowa2Context> options)
            : base(options)
        {
        }

        public DbSet<Aplikacja_webowa2.Models.Music> Music { get; set; } = default!;
    }
}
