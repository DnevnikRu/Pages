using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;

namespace Pages.Data.Repositories
{
    public class EFRepository<TEntity, TIdentifier,TContext> : IRepository<TEntity, TIdentifier>
        where TEntity : class, IEntity<TIdentifier>
        where TIdentifier : class
        where TContext: DbContext
    {

        private readonly DbContext context;

        public EFRepository(String connectionString)
        {
            Database.SetInitializer<TContext>(new CreateDatabaseIfNotExists<TContext>());
            this.context = (DbContext)Activator.CreateInstance(typeof(TContext), new object[] { connectionString });
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
            context.Set<TEntity>().Attach(entity);
                
            if ( this.context.Set<TEntity>().Any(e => e.Id == entity.Id) )
                this.context.Entry<TEntity>(entity).State = System.Data.EntityState.Modified;
            else
                this.context.Entry<TEntity>(entity).State = System.Data.EntityState.Added;

            this.context.SaveChanges();
        }

        public IQueryable<TEntity> Objects
        {
            get
            {
                return this.context.Set<TEntity>();
            }
        }

        public void Delete(TEntity entity)
        {
            this.context.Set<TEntity>().Attach(entity);
            this.context.Entry<TEntity>(entity).State = System.Data.EntityState.Deleted;
            this.context.SaveChanges();
        }
    }
}
