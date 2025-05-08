using EShopApplication.Domain.DomainModels;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
        E? Get<E>(Expression<Func<T, E>> selector,//which part of the entity (T) should be selected as the output (E).
            Expression<Func<T, bool>>? predicate = null, //get (T), WHERE you have a condition (bool) part of the line
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null); //eager loading - means that loads everything
                                                                                   //so if we make something where we specify the include, we get everything 
                                                                                   //if we want to get a shopping cart, and we specify get(selector, predicate, orderBy, WE INCLUDE THE PRODUCTS) - it will load all of them
        IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        //      Get<E> E? (Single result) Returns a single entity or value.
        //      GetAll<E> IEnumerable<E>(Collection of results)  Returns multiple results(a list)
    }
}
