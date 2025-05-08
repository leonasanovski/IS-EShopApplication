using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EShopApplication.Domain.DomainModels;

namespace EShopApplication.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;// all of the data from the entities
        private readonly DbSet<T>? entities;// all of the entitites like Product, ShoppingCart and ProductInShoppingCart
        //The dbSet is a table in the database for querying and CRUD operations.
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.entities = _context.Set<T>();//they are in different DbSet for all of the entities in the ApplicationDbContext
        }

        T IRepository<T>.Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        E? IRepository<T>.Get<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate, Func<IQueryable<T>,
                IOrderedQueryable<T>>? orderBy, Func<IQueryable<T>,
                    IIncludableQueryable<T, object>>? include) where E : default
        {
            IQueryable<T> query = entities;
            //actually I am going to querry the entities in the background, dependent what is needed (what type of selection, is there a condition,
            //is there a way to order them and shall i include something or not
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderBy != null)
            {
                return orderBy(query).Select(selector).FirstOrDefault();
            }
            return query.Select(selector).FirstOrDefault();
        }

        IEnumerable<E> IRepository<T>.GetAll<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include)
        {
            IQueryable<T> query = entities;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderBy != null)
            {
                return orderBy(query).Select(selector).AsEnumerable();
            }
            return query.Select(selector).AsEnumerable();
        }

        T IRepository<T>.Insert(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        T IRepository<T>.Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
