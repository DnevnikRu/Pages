using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using Pages.Data;

namespace Pages.Data.Repositories
{
    public class InMemoryRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier> 
        where TEntity : IEntity<TIdentifier>
        where TIdentifier : class
    {
        readonly IList<TEntity> objects = new List<TEntity>();

        public void Save(TEntity entity)
        {
            this.Delete(entity);

            this.objects.Add(entity);
        }

        public IQueryable<TEntity> Objects
        {
            get { return this.objects.AsQueryable(); }
        }

        public void Delete(TEntity entity)
        {
            var existing = this.objects.FirstOrDefault(e => e.Id == (entity.Id));

            if (null != existing)
                this.objects.Remove(existing);
        }
    }
}