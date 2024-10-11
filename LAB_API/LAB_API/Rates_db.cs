using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_API
{
        public class Rates_db : DbContext
        {
            public virtual DbSet<Rates> Curency_rates { get; set; }
        }
}
