using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Pages.Data.Repositories
{
    public class EFRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
        where TEntity : class, IEntity<TIdentifier>
        where TIdentifier : class
    {

        private readonly DbContext context;

        public EFRepository(DbContext context)
        {
            //Database.SetInitializer<TContext>(new CreateDatabaseIfNotExists<TContext>());
            //context = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { connectionString });

            this.context = context;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return this.context.Set<TEntity>();
            }
        }

        public void Save(TEntity entity)
        {            
            this.context.Entry<TEntity>(entity).State = System.Data.EntityState.Modified;
            this.context.SaveChanges();
        }

        public IQueryable<TEntity> Objects
        {
            get { return this.DbSet.AsQueryable(); }
        }

        public void Delete(TEntity entity)
        {
            this.context.Entry<TEntity>(entity).State = System.Data.EntityState.Deleted;
            this.context.SaveChanges();
        }
    }
}
