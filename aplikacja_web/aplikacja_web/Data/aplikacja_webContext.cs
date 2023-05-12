using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aplikacja_web.models;

namespace aplikacja_web.Data
{
    public class aplikacja_webContext : DbContext
    {
        public aplikacja_webContext (DbContextOptions<aplikacja_webContext> options)
            : base(options)
        {
        }

        public DbSet<aplikacja_web.models.whisky> whisky { get; set; } = default!;
    }
}
