﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pages.Data.Repositories
{
    public interface IRepository<TEntity, TIdentifier> 
        where TEntity : class, IEntity<TIdentifier>
        where TIdentifier : class
    {
        void Save(TEntity entity);
        IQueryable<TEntity> Objects { get; }
        void Delete(TEntity entity);
        //TEntity Get(TIdentifier id);
    }
}