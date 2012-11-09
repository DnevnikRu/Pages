using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pages.Models;
using Pages.Data;

namespace Pages.Controllers
{
    public abstract class ApiController<TResource, TIdentifier> : ApiController where TResource : IEntity<TIdentifier> where TIdentifier : class
    {
        protected readonly IRepository<TResource, TIdentifier> repo;

        public ApiController(IRepository<TResource, TIdentifier> repo)
        {
            this.repo = repo;
        }

        public TResource Get(TIdentifier id)
        {
            var entity = this.repo.Objects.FirstOrDefault(e => e.Id == id);

            if (entity == null)
                throw new ResourceNotFoundException<TResource, TIdentifier>(id);

            return entity;
        }

        public IQueryable<TResource> Get()
        {
            return this.repo.Objects;
        }

        public void Delete(TIdentifier id)
        {
            var entity = this.Get(id);

            this.repo.Delete(entity);
        }

        public void Post(TResource entity)
        {
            this.repo.Save(entity);
        }

        public void Put(TIdentifier id, TResource entity)
        {
            entity.Id = id;

            this.repo.Save(entity);
        }
    }
}
