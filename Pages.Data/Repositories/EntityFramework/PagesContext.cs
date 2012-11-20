using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Pages.Data;
using Pages.Data.Entities;

namespace Pages.Data.Repositories.EF
{
    class PagesContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public PagesContext() { }
    }
}
