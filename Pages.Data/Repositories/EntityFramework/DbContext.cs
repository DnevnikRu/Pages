using Pages.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;

namespace Pages.Data.Repositories.EntityFramework
{
    public abstract class DbContext : System.Data.Entity.DbContext
    {
        public DbContext (String connectionString) : base(connectionString)
	    {
            (this as IObjectContextAdapter).ObjectContext.SavingChanges += Context_SavingChanges;
	    }

        void Context_SavingChanges(object sender, EventArgs e)
        {
            ((ObjectContext)sender).ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                .Select( entry => entry.Entity )
                .Where(entity => entity is IHasModifiedDatetime)
                .Cast<IHasModifiedDatetime>()
                .ToList().ForEach(entity => entity.ModifiedAt = DateTime.Now);
        }
    }
}
