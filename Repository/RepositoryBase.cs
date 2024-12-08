﻿using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            if (!trackChanges)
                return RepositoryContext.Set<T>().AsNoTracking();
            else
                return RepositoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges)
        {
            if (!trackChanges)
                return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
            else
                return RepositoryContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
            => RepositoryContext.Add(entity);
        public void Update(T entity)
            => RepositoryContext.Update(entity);
        public void Delete(T entity)
            => RepositoryContext.Remove(entity);
    }
}
